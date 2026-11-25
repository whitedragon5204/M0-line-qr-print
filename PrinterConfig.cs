using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Printing;
using System.Reflection;
using System.Text;

namespace PackLabelPrint
{
    public static class PrinterConfig
    {
        static bool _autoTried;

        public static void TryApplyOnce(string printer)
        {
            if (_autoTried)
            {
                return;
            }
            _autoTried = true;
            try
            {
                ApplyTo(printer);
            }
            catch
            {
            }
        }

        public static string Apply()
        {
            string printer = FindPrinter();
            if (string.IsNullOrEmpty(printer))
            {
                throw new Exception("本机没有 TSC TTP-244 Pro。先装 Seagull 驱动，队列名字必须和设备和打印机里一致。");
            }
            return ApplyTo(printer);
        }

        static string ApplyTo(string printer)
        {
            StringBuilder log = new StringBuilder();
            bool ok = false;

            try
            {
                ApplyTicket(printer);
                log.AppendLine("已写入纸张 USER、纵向、间隙 3mm。");
                ok = true;
            }
            catch (Exception ex)
            {
                log.AppendLine("打印票失败：" + ex.Message);
            }

            string dat = FindBesideExe("TSC本机配置.dat");
            if (!string.IsNullOrEmpty(dat))
            {
                try
                {
                    ApplyDat(printer, dat);
                    log.AppendLine("已写入驱动数据包。");
                    ok = true;
                }
                catch (Exception ex)
                {
                    log.AppendLine("数据包失败：" + ex.Message);
                }
            }

            if (!ok)
            {
                throw new Exception(log.ToString());
            }
            return "已写入打印机：" + printer + "\r\n" + log.ToString().Trim()
                + "\r\n打开打印首选项核对：纸张 USER 101.6×152.4，方向纵向。";
        }

        public static string FindPrinter()
        {
            string want = LabelPrinter.PrinterName;
            foreach (string name in PrinterSettings.InstalledPrinters)
            {
                if (string.Equals(name, want, StringComparison.OrdinalIgnoreCase))
                {
                    return name;
                }
            }
            foreach (string name in PrinterSettings.InstalledPrinters)
            {
                if (name.IndexOf("TTP-244", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return name;
                }
            }
            return "";
        }

        static void ApplyTicket(string printer)
        {
            using (Stream stream = OpenTicket())
            using (LocalPrintServer server = new LocalPrintServer())
            using (PrintQueue queue = server.GetPrintQueue(printer))
            {
                PrintTicket ticket = new PrintTicket(stream);
                ValidationResult result = queue.MergeAndValidatePrintTicket(queue.UserPrintTicket, ticket);
                queue.UserPrintTicket = result.ValidatedPrintTicket;
                try
                {
                    queue.DefaultPrintTicket = result.ValidatedPrintTicket;
                }
                catch
                {
                }
                queue.Commit();
            }
        }

        static Stream OpenTicket()
        {
            Stream embedded = Assembly.GetExecutingAssembly().GetManifestResourceStream("TSC本机配置.xml");
            if (embedded != null)
            {
                return embedded;
            }
            string path = FindBesideExe("TSC本机配置.xml");
            if (string.IsNullOrEmpty(path))
            {
                throw new Exception("找不到 TSC本机配置.xml");
            }
            return File.OpenRead(path);
        }

        static string FindBesideExe(string fileName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (File.Exists(path))
            {
                return path;
            }
            return "";
        }

        static void ApplyDat(string printer, string datPath)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = Path.Combine(Environment.SystemDirectory, "cmd.exe");
            psi.Arguments = "/c rundll32 printui.dll,PrintUIEntry /Sr /n \"" + printer + "\" /a \"" + datPath + "\" d g u r";
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            using (Process process = Process.Start(psi))
            {
                if (process != null)
                {
                    process.WaitForExit(20000);
                }
            }
        }
    }
}