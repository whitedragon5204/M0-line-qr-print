using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PackLabelPrint
{
    public static class AppLog
    {
        public static string LogDir
        {
            get { return Path.Combine(Application.StartupPath, "logs"); }
        }

        public static string TodayLogFile
        {
            get { return Path.Combine(LogDir, "pack-label-" + DateTime.Now.ToString("yyyyMMdd") + ".log"); }
        }

        public static string TodayCodesFile
        {
            get { return Path.Combine(LogDir, "codes-" + DateTime.Now.ToString("yyyyMMdd") + ".txt"); }
        }

        public static void Write(string line)
        {
            Directory.CreateDirectory(LogDir);
            File.AppendAllText(TodayLogFile, line + Environment.NewLine);
        }

        public static void RememberCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return;
            }
            Directory.CreateDirectory(LogDir);
            File.AppendAllText(TodayCodesFile, DateTime.Now.ToString("HH:mm:ss") + "\t" + code.Trim() + Environment.NewLine);
        }

        public static List<string> LoadTodayCodes()
        {
            List<string> list = new List<string>();
            if (!File.Exists(TodayCodesFile))
            {
                return list;
            }
            foreach (string line in File.ReadAllLines(TodayCodesFile))
            {
                string code = line;
                int tab = line.IndexOf('\t');
                if (tab >= 0 && tab < line.Length - 1)
                {
                    code = line.Substring(tab + 1);
                }
                code = code.Trim();
                if (code.Length == 0)
                {
                    continue;
                }
                list.Remove(code);
                list.Insert(0, code);
            }
            return list;
        }
    }
}
