using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PackLabelPrint
{
    public static class AppLog
    {
        public const int KeepDays = 7;

        public static string LogDir
        {
            get { return Path.Combine(Application.StartupPath, "logs"); }
        }

        public static string Dir(string kind)
        {
            string name = string.Equals(kind, "scan", StringComparison.OrdinalIgnoreCase) ? "scan" : "mes";
            return Path.Combine(LogDir, name);
        }

        public static void PurgeOld()
        {
            DateTime keepFrom = DateTime.Today.AddDays(1 - KeepDays);
            PurgeDir(LogDir, keepFrom);
            PurgeDir(Dir("mes"), keepFrom);
            PurgeDir(Dir("scan"), keepFrom);
        }

        static void PurgeDir(string dir, DateTime keepFrom)
        {
            if (!Directory.Exists(dir))
            {
                return;
            }
            foreach (string file in Directory.GetFiles(dir))
            {
                string name = Path.GetFileNameWithoutExtension(file);
                int dash = name.LastIndexOf('-');
                if (dash < 0 || dash + 9 > name.Length)
                {
                    continue;
                }
                DateTime day;
                if (!DateTime.TryParseExact(name.Substring(dash + 1, 8), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out day))
                {
                    continue;
                }
                if (day < keepFrom)
                {
                    try { File.Delete(file); } catch { }
                }
            }
        }

        public static void Write(string kind, string line)
        {
            string dir = Dir(kind);
            Directory.CreateDirectory(dir);
            string file = Path.Combine(dir, kind + "-" + DateTime.Now.ToString("yyyyMMdd") + ".log");
            File.AppendAllText(file, line + Environment.NewLine);
        }

        public static void RememberPrint(string kind, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }
            string dir = Dir(kind);
            Directory.CreateDirectory(dir);
            string file = Path.Combine(dir, "history-" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            File.AppendAllText(file, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + text.Trim() + Environment.NewLine);
        }

        public static List<string> LoadWeekHistory(string kind)
        {
            List<HistRow> rows = new List<HistRow>();
            string dir = Dir(kind);
            Directory.CreateDirectory(dir);
            for (int i = 0; i < KeepDays; i++)
            {
                DateTime day = DateTime.Today.AddDays(-i);
                string ymd = day.ToString("yyyyMMdd");
                AddFileRows(Path.Combine(dir, "history-" + ymd + ".txt"), day, rows);
                AddFileRows(Path.Combine(LogDir, "history-" + kind + "-" + ymd + ".txt"), day, rows);
                if (kind == "mes")
                {
                    AddOldCodesRows(Path.Combine(LogDir, "codes-" + ymd + ".txt"), day, rows);
                    AddOldCodesRows(Path.Combine(dir, "codes-" + ymd + ".txt"), day, rows);
                }
            }
            return GroupDisplay(rows);
        }

        public static bool PrintedThisWeek(string kind, string code)
        {
            code = (code ?? "").Trim();
            if (code.Length == 0)
            {
                return false;
            }
            foreach (string line in LoadWeekHistory(kind))
            {
                if (string.Equals(HistoryCode(line), code, StringComparison.Ordinal))
                {
                    return true;
                }
            }
            return false;
        }

        static void AddFileRows(string file, DateTime day, List<HistRow> rows)
        {
            if (!File.Exists(file))
            {
                return;
            }
            foreach (string raw in File.ReadAllLines(file))
            {
                HistRow row;
                if (TryParseRow(raw, day, out row))
                {
                    rows.Add(row);
                }
            }
        }

        static void AddOldCodesRows(string file, DateTime day, List<HistRow> rows)
        {
            AddFileRows(file, day, rows);
        }

        static bool TryParseRow(string raw, DateTime day, out HistRow row)
        {
            row = null;
            string line = (raw ?? "").Trim();
            if (line.Length == 0)
            {
                return false;
            }
            string timePart = "";
            string codePart = line;
            int tab = line.IndexOf('\t');
            if (tab >= 0)
            {
                timePart = line.Substring(0, tab).Trim();
                codePart = line.Substring(tab + 1).Trim();
            }
            else
            {
                int sp = line.IndexOf("  ");
                if (sp >= 0)
                {
                    timePart = line.Substring(0, sp).Trim();
                    codePart = line.Substring(sp + 2).Trim();
                }
            }
            codePart = StripCount(codePart);
            if (codePart.Length == 0)
            {
                return false;
            }
            DateTime when = day;
            DateTime parsed;
            if (DateTime.TryParse(timePart, out parsed))
            {
                when = parsed;
            }
            else
            {
                TimeSpan clock;
                if (TimeSpan.TryParse(timePart, out clock))
                {
                    when = day.Date.Add(clock);
                }
            }
            row = new HistRow();
            row.When = when;
            row.Code = codePart;
            return true;
        }

        static List<string> GroupDisplay(List<HistRow> rows)
        {
            Dictionary<string, HistRow> latest = new Dictionary<string, HistRow>(StringComparer.Ordinal);
            Dictionary<string, int> counts = new Dictionary<string, int>(StringComparer.Ordinal);
            foreach (HistRow row in rows)
            {
                int n;
                if (counts.TryGetValue(row.Code, out n))
                {
                    counts[row.Code] = n + 1;
                }
                else
                {
                    counts[row.Code] = 1;
                }
                HistRow old;
                if (!latest.TryGetValue(row.Code, out old) || row.When >= old.When)
                {
                    latest[row.Code] = row;
                }
            }
            List<HistRow> uniq = new List<HistRow>(latest.Values);
            uniq.Sort(CompareNewerFirst);
            List<string> list = new List<string>();
            foreach (HistRow row in uniq)
            {
                list.Add(row.When.ToString("yyyy-MM-dd HH:mm:ss") + "  " + row.Code + "  " + counts[row.Code]);
            }
            return list;
        }

        static int CompareNewerFirst(HistRow a, HistRow b)
        {
            return b.When.CompareTo(a.When);
        }

        static string StripCount(string codePart)
        {
            if (string.IsNullOrEmpty(codePart))
            {
                return "";
            }
            int sp = codePart.LastIndexOf("  ");
            if (sp < 0)
            {
                return codePart.Trim();
            }
            int n;
            if (int.TryParse(codePart.Substring(sp + 2).Trim(), out n))
            {
                return codePart.Substring(0, sp).Trim();
            }
            return codePart.Trim();
        }

        public static string HistoryCode(string displayLine)
        {
            if (string.IsNullOrWhiteSpace(displayLine))
            {
                return "";
            }
            int sp = displayLine.IndexOf("  ");
            if (sp >= 0 && sp + 2 < displayLine.Length)
            {
                DateTime dummy;
                if (DateTime.TryParse(displayLine.Substring(0, sp).Trim(), out dummy))
                {
                    return StripCount(displayLine.Substring(sp + 2).Trim());
                }
            }
            return StripCount(displayLine.Trim());
        }

        class HistRow
        {
            public DateTime When;
            public string Code;
        }
    }
}
