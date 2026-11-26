using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Management;
using System.Threading;
using ZXing;
using ZXing.QrCode;

namespace PackLabelPrint
{
    public static class LabelPrinter
    {
        const int PrinterStateOffline = 0x80;
        const int PrinterStateError = 0x2;
        const int PrinterStateNotAvailable = 0x1000;
        const int PrinterStatusOffline = 7;

        public static string PrinterName
        {
            get
            {
                string name = ConfigurationManager.AppSettings["PrinterName"];
                return name == null ? "" : name.Trim();
            }
        }

        public static void Print(string packCode, string customerCode)
        {
            if (string.IsNullOrWhiteSpace(packCode))
            {
                throw new Exception("没有可打印的 PACK 码");
            }
            if (string.IsNullOrWhiteSpace(customerCode))
            {
                throw new Exception("没有可打印的客户码");
            }

            string printer = ResolveInstalledName(PrinterName);
            WaitUntilIdle(printer);
            EnsurePrinterReady(printer);
            PrintByTspl(printer, packCode, customerCode);
            WaitUntilIdle(printer);
        }

        public static void PrintSpecified(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new Exception("没有可打印的指定码");
            }

            string printer = ResolveInstalledName(PrinterName);
            WaitUntilIdle(printer);
            EnsurePrinterReady(printer);
            PrintByTsplSpecified(printer, code.Trim());
            WaitUntilIdle(printer);
        }

        static string ResolveInstalledName(string configured)
        {
            if (string.IsNullOrEmpty(configured))
            {
                throw new Exception("App.config 缺少 PrinterName");
            }

            foreach (string installed in PrinterSettings.InstalledPrinters)
            {
                if (string.Equals(installed, configured, StringComparison.OrdinalIgnoreCase))
                {
                    return installed;
                }
            }
            throw new Exception("本机没有这台打印机：" + configured + "。打开 App.config 改 PrinterName，改成设备和打印机里的名字。");
        }

        static void EnsurePrinterReady(string printer)
        {
            PrinterInfo info = FindPrinter(printer);
            if (info == null)
            {
                throw new Exception("找不到打印机队列：" + printer);
            }

            if (info.WorkOffline)
            {
                TryClearWorkOffline(printer);
                info = FindPrinter(printer);
                if (info == null)
                {
                    throw new Exception("找不到打印机队列：" + printer);
                }
            }

            bool workOffline = info.WorkOffline;
            int printerStatus = info.PrinterStatus;
            int printerState = info.PrinterState;
            int detectedError = info.DetectedError;

            if (workOffline)
            {
                throw new Exception("打印机处于脱机。已尝试取消队列脱机，仍未恢复。看设备和打印机里是否勾了「脱机使用」，或线是否拔了。");
            }
            if (printerStatus == PrinterStatusOffline)
            {
                throw new Exception("打印机离线（可能已拔线）。");
            }
            if ((printerState & PrinterStateOffline) != 0 ||
                (printerState & PrinterStateError) != 0 ||
                (printerState & PrinterStateNotAvailable) != 0)
            {
                throw new Exception("打印机故障或不可用，状态值 " + printerState + "。");
            }
            if (detectedError >= 3)
            {
                throw new Exception("打印机报错 DetectedErrorState=" + detectedError + "。");
            }

            string port = info.PortName;
            if (printer.IndexOf("TSC", StringComparison.OrdinalIgnoreCase) >= 0 ||
                port.StartsWith("USB", StringComparison.OrdinalIgnoreCase))
            {
                if (!UsbPrinterPresent())
                {
                    throw new Exception("USB 打印机已拔线或不在线，未真正打出。");
                }
            }
        }

        static void EnsureJobNotError(string printer)
        {
            string like = EscapeWql(printer) + "%";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                "SELECT * FROM Win32_PrintJob WHERE Name LIKE '" + like + "'"))
            {
                foreach (ManagementObject job in searcher.Get())
                {
                    string status = job["Status"] == null ? "" : job["Status"].ToString();
                    int statusMask = job["StatusMask"] == null ? 0 : Convert.ToInt32(job["StatusMask"]);
                    if (status.IndexOf("Error", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (statusMask & 2) != 0)
                    {
                        throw new Exception("打印队列作业失败：" + status);
                    }
                }
            }
        }

        static void CancelJobs(string printer)
        {
            try
            {
                string like = EscapeWql(printer) + "%";
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                    "SELECT * FROM Win32_PrintJob WHERE Name LIKE '" + like + "'"))
                {
                    foreach (ManagementObject job in searcher.Get())
                    {
                        job.Delete();
                    }
                }
            }
            catch
            {
            }
        }

        class PrinterInfo
        {
            public bool WorkOffline;
            public int PrinterStatus;
            public int PrinterState;
            public int DetectedError;
            public string PortName;
        }

        static void TryClearWorkOffline(string printer)
        {
            try
            {
                string q = "SELECT * FROM Win32_Printer WHERE Name = '" + EscapeWql(printer) + "'";
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(q))
                {
                    foreach (ManagementObject mo in searcher.Get())
                    {
                        mo["WorkOffline"] = false;
                        mo.Put();
                        return;
                    }
                }
            }
            catch
            {
            }
        }

        static PrinterInfo FindPrinter(string printer)
        {
            string q = "SELECT WorkOffline, PrinterStatus, PrinterState, DetectedErrorState, PortName FROM Win32_Printer WHERE Name = '" + EscapeWql(printer) + "'";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(q))
            {
                foreach (ManagementObject mo in searcher.Get())
                {
                    PrinterInfo info = new PrinterInfo();
                    info.WorkOffline = mo["WorkOffline"] != null && Convert.ToBoolean(mo["WorkOffline"]);
                    info.PrinterStatus = mo["PrinterStatus"] == null ? 0 : Convert.ToInt32(mo["PrinterStatus"]);
                    info.PrinterState = mo["PrinterState"] == null ? 0 : Convert.ToInt32(mo["PrinterState"]);
                    info.DetectedError = mo["DetectedErrorState"] == null ? 0 : Convert.ToInt32(mo["DetectedErrorState"]);
                    info.PortName = mo["PortName"] == null ? "" : mo["PortName"].ToString();
                    return info;
                }
            }
            return null;
        }

        static bool UsbPrinterPresent()
        {
            string[] queries = new string[]
            {
                "SELECT Name, Status, ConfigManagerErrorCode, PNPDeviceID FROM Win32_PnPEntity WHERE PNPDeviceID LIKE 'USBPRINT%TTP-244%'",
                "SELECT Name, Status, ConfigManagerErrorCode, PNPDeviceID FROM Win32_PnPEntity WHERE PNPDeviceID LIKE 'USB\\\\VID_1203&PID_0172%'",
                "SELECT Name, Status, ConfigManagerErrorCode, PNPDeviceID FROM Win32_PnPEntity WHERE Name LIKE '%TTP-244%'"
            };

            foreach (string q in queries)
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(q))
                {
                    foreach (ManagementObject mo in searcher.Get())
                    {
                        int err = mo["ConfigManagerErrorCode"] == null ? 0 : Convert.ToInt32(mo["ConfigManagerErrorCode"]);
                        string status = mo["Status"] == null ? "" : mo["Status"].ToString();
                        if (err == 0 && (status.Length == 0 || string.Equals(status, "OK", StringComparison.OrdinalIgnoreCase)))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        static string EscapeWql(string value)
        {
            return value.Replace("\\", "\\\\").Replace("'", "\\'");
        }

        static void PrintByTspl(string printer, string packCode, string customerCode)
        {
            string widthMm = CfgText("LabelWidthMm", "50");
            string heightMm = CfgText("LabelHeightMm", "40");
            string gapMm = CfgText("LabelGapMm", "3");
            int widthDots = MmToDots(widthMm, 80);
            int heightDots = MmToDots(heightMm, 80);
            widthDots = widthDots - (widthDots % 8);
            int layoutH = MmToDots(CfgText("PrintBandMm", "20"), 80);
            if (layoutH > heightDots)
            {
                layoutH = heightDots;
            }
            int packDown = MmToDots(CfgText("PackDownMm", "5"), 0);
            int custDown = MmToDots(CfgText("CustDownMm", "10"), 0);

            Bitmap bmp = new Bitmap(widthDots, heightDots);
            try
            {
                bmp.SetResolution(203f, 203f);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    DrawLabel(g, packCode, customerCode, widthDots, layoutH, packDown, custDown);
                }
                TsplPrinter.SendBitmap(printer, ToMono(bmp), widthDots, heightDots, widthMm, heightMm, gapMm);
            }
            finally
            {
                bmp.Dispose();
            }
        }

        static void PrintByTsplSpecified(string printer, string code)
        {
            string widthMm = CfgText("LabelWidthMm", "50");
            string heightMm = CfgText("LabelHeightMm", "40");
            string gapMm = CfgText("LabelGapMm", "3");
            int widthDots = MmToDots(widthMm, 80);
            int heightDots = MmToDots(heightMm, 80);
            widthDots = widthDots - (widthDots % 8);

            Bitmap bmp = new Bitmap(widthDots, heightDots);
            try
            {
                bmp.SetResolution(203f, 203f);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    DrawSpecified(g, code, widthDots, heightDots);
                }
                TsplPrinter.SendBitmap(printer, ToMono(bmp), widthDots, heightDots, widthMm, heightMm, gapMm);
            }
            finally
            {
                bmp.Dispose();
            }
        }

        static void DrawSpecified(Graphics g, string code, int paperW, int paperH)
        {
            g.PageUnit = GraphicsUnit.Pixel;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;

            int inset = Math.Max(8, paperW / 40);
            int qr = paperW * 16 / 50;
            if (qr > paperH - inset * 2)
            {
                qr = paperH - inset * 2;
            }
            int pad = Math.Max(6, paperW / 50);
            int x1 = inset;
            int y = Math.Max(inset, (paperH - qr) / 2);
            DrawQr(g, code, x1, y, qr);
            float textX = x1 + qr + pad;
            float textW = paperW - textX - inset;
            if (textW < 20f)
            {
                textW = 20f;
            }
            string[] lines = SplitTwo(code);
            float gap = 6f;
            float lineH = Math.Max(24f, (qr - gap) / 2f);
            float block = lines.Length * lineH + (lines.Length - 1) * gap;
            float textY = y + Math.Max(0f, (qr - block) / 2f);
            for (int i = 0; i < lines.Length; i++)
            {
                DrawCode(g, lines[i], textX, textY + i * (lineH + gap), textW, lineH);
            }
        }

        static string[] SplitTwo(string text)
        {
            text = text ?? "";
            if (text.Length <= 1)
            {
                return new string[] { text };
            }
            int mid = (text.Length + 1) / 2;
            return new string[] { text.Substring(0, mid), text.Substring(mid) };
        }

        static void WaitUntilIdle(string printer)
        {
            DateTime until = DateTime.Now.AddSeconds(20);
            while (DateTime.Now < until)
            {
                if (!HasJobs(printer))
                {
                    Thread.Sleep(1200);
                    return;
                }
                Thread.Sleep(200);
            }
        }

        static bool HasJobs(string printer)
        {
            string like = EscapeWql(printer) + "%";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                "SELECT JobId FROM Win32_PrintJob WHERE Name LIKE '" + like + "'"))
            {
                foreach (ManagementObject job in searcher.Get())
                {
                    return true;
                }
            }
            return false;
        }

        static int MmToDots(string mmText, int minDots)
        {
            double mm;
            if (!double.TryParse(mmText, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out mm))
            {
                mm = 0;
            }
            int dots = (int)Math.Round(mm * 8.0);
            if (dots < minDots)
            {
                dots = minDots;
            }
            return dots;
        }

        static byte[] ToMono(Bitmap bmp)
        {
            int w = bmp.Width;
            int h = bmp.Height;
            int rowBytes = (w + 7) / 8;
            byte[] data = new byte[rowBytes * h];
            for (int y = 0; y < h; y++)
            {
                int row = y * rowBytes;
                for (int x = 0; x < w; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    if (c.R + c.G + c.B >= 384)
                    {
                        data[row + (x / 8)] |= (byte)(0x80 >> (x % 8));
                    }
                }
            }
            return data;
        }

        static string CfgText(string key, string fallback)
        {
            string s = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(s))
            {
                return fallback;
            }
            return s.Trim();
        }

        static int CfgInt(string key, int fallback)
        {
            int n;
            string s = ConfigurationManager.AppSettings[key];
            if (int.TryParse(s, out n))
            {
                return n;
            }
            return fallback;
        }

        static void DrawLabel(Graphics g, string packCode, string customerCode, int paperW, int paperH, int packDown, int custDown)
        {
            g.Clear(Color.White);
            g.PageUnit = GraphicsUnit.Pixel;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;

            int inset = Math.Max(8, paperW / 40);
            int qrPack = paperH * 14 / 20;
            int qrCust = paperH * 11 / 20;
            if (qrPack > paperH - inset * 2)
            {
                qrPack = paperH - inset * 2;
            }
            if (qrCust > paperH - inset * 2)
            {
                qrCust = paperH - inset * 2;
            }
            int pad = Math.Max(6, paperW / 50);
            int midGap = Math.Max(10, paperW / 30);
            int x1 = inset;
            int x2 = paperW - inset - qrCust;
            int yPack = inset + (paperH - inset * 2 - qrPack) / 2 + packDown;
            int yCust = inset + (paperH - inset * 2 - qrCust) / 2 + custDown;

            DrawQr(g, packCode, x1, yPack, qrPack);
            DrawQr(g, customerCode, x2, yCust, qrCust);

            float innerLeft = x1 + qrPack + pad;
            float innerRight = x2 - pad;
            float textW = (innerRight - innerLeft - midGap) / 2f;
            float packX = innerLeft;
            float custX = innerRight - textW;

            DrawTitleAndCode(g, "PACK码", packCode, packX, yPack, textW, qrPack);
            DrawTitleAndCode(g, "客户码", customerCode, custX, yCust, textW, qrCust);
        }

        static void DrawQr(Graphics g, string content, int x, int y, int size)
        {
            Bitmap qr = MakeQr(content);
            try
            {
                g.DrawImage(qr, x, y, size, size);
            }
            finally
            {
                qr.Dispose();
            }
        }

        static void DrawTitleAndCode(Graphics g, string title, string code, float x, float y, float width, int blockH)
        {
            string[] lines = SplitLines(code);
            float titleH = Math.Max(14f, blockH / 7f);
            float lineH = Math.Max(16f, blockH / 5.5f);
            float block = titleH + lines.Length * lineH;
            float y0 = y + Math.Max(0, (blockH - block) / 2f);

            using (Font titleFont = new Font("微软雅黑", titleH * 0.75f, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                DrawCentered(g, title, titleFont, x, y0, width, titleH);
            }

            for (int i = 0; i < lines.Length; i++)
            {
                DrawCode(g, lines[i], x, y0 + titleH + i * lineH, width, lineH);
            }
        }

        static void DrawCentered(Graphics g, string text, Font font, float x, float y, float width, float height)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                sf.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap;
                sf.Trimming = StringTrimming.None;
                g.DrawString(text, font, Brushes.Black, new RectangleF(x, y, width, height), sf);
            }
        }

        static void DrawCode(Graphics g, string text, float x, float y, float width, float height)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            float size = Math.Max(12f, height * 0.9f);
            Font font = new Font("Consolas", size, FontStyle.Bold, GraphicsUnit.Pixel);
            try
            {
                while (size > 10f && g.MeasureString(text, font).Width > width)
                {
                    font.Dispose();
                    size -= 1f;
                    font = new Font("Consolas", size, FontStyle.Bold, GraphicsUnit.Pixel);
                }
                DrawCentered(g, text, font, x, y, width, height);
            }
            finally
            {
                font.Dispose();
            }
        }

        static string[] SplitLines(string text)
        {
            text = text ?? "";
            if (text.Length <= 10)
            {
                return new string[] { text };
            }
            if (text.Length <= 16)
            {
                int mid = (text.Length + 1) / 2;
                return new string[] { text.Substring(0, mid), text.Substring(mid) };
            }
            int n = (text.Length + 2) / 3;
            return new string[]
            {
                text.Substring(0, n),
                text.Substring(n, n),
                text.Substring(n + n)
            };
        }

        static Bitmap MakeQr(string content)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options.Width = 220;
            options.Height = 220;
            options.Margin = 0;
            options.CharacterSet = "UTF-8";
            writer.Options = options;
            return writer.Write(content);
        }
    }
}
