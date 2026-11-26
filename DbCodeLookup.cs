using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace PackLabelPrint
{
    public static class DbCodeLookup
    {
        public static string Cfg(string key)
        {
            string s = ConfigurationManager.AppSettings[key];
            return s == null ? "" : s.Trim();
        }

        public static void Save(
            string host, string port, string user, string password,
            string dbName, string table, string field)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            Set(config, "DbHost", host);
            Set(config, "DbPort", port);
            Set(config, "DbUser", user);
            Set(config, "DbPassword", password);
            Set(config, "DbName", dbName);
            Set(config, "DbTable", table);
            Set(config, "DbField", field);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        static void Set(Configuration config, string key, string value)
        {
            if (config.AppSettings.Settings[key] == null)
            {
                config.AppSettings.Settings.Add(key, value ?? "");
            }
            else
            {
                config.AppSettings.Settings[key].Value = value ?? "";
            }
        }

        public static string Test(string host, string port, string user, string password, string dbName, string table, string field)
        {
            using (MySqlConnection conn = Open(host, port, user, password, dbName))
            {
                if (!string.IsNullOrWhiteSpace(table))
                {
                    CheckName(table, "表名");
                    string sql = "SELECT 1 FROM " + Quote(table) + " LIMIT 1";
                    if (!string.IsNullOrWhiteSpace(field))
                    {
                        CheckName(field, "字段名");
                        sql = "SELECT " + Quote(field) + " FROM " + Quote(table) + " LIMIT 1";
                    }
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.ExecuteScalar();
                    }
                }
            }
            return "连接成功";
        }

        public static bool Exists(string code, out string message)
        {
            message = "";
            code = (code ?? "").Trim();
            if (code.Length == 0)
            {
                message = "没有要查询的码";
                return false;
            }

            string table = Cfg("DbTable");
            string field = Cfg("DbField");
            if (string.IsNullOrWhiteSpace(table) || string.IsNullOrWhiteSpace(field))
            {
                message = "还没配齐表名和字段名，不能查库打印。";
                return false;
            }
            CheckName(table, "表名");
            CheckName(field, "字段名");

            using (MySqlConnection conn = Open(Cfg("DbHost"), Cfg("DbPort"), Cfg("DbUser"), Cfg("DbPassword"), Cfg("DbName")))
            using (MySqlCommand cmd = new MySqlCommand(
                "SELECT " + Quote(field) + " FROM " + Quote(table) + " WHERE " + Quote(field) + "=@c LIMIT 1", conn))
            {
                cmd.Parameters.AddWithValue("@c", code);
                object found = cmd.ExecuteScalar();
                if (found == null || found == DBNull.Value)
                {
                    message = "库里没有这颗码，未打印。";
                    return false;
                }
                message = "库里有这颗码";
                return true;
            }
        }

        static MySqlConnection Open(string host, string port, string user, string password, string dbName)
        {
            if (string.IsNullOrWhiteSpace(host))
            {
                throw new Exception("还没填数据库主机");
            }
            int p;
            if (!int.TryParse(string.IsNullOrWhiteSpace(port) ? "3306" : port, out p))
            {
                p = 3306;
            }
            string connStr = "Server=" + host.Trim()
                + ";Port=" + p
                + ";User Id=" + (user ?? "")
                + ";Password=" + (password ?? "")
                + ";Charset=utf8;SslMode=None;Connection Timeout=8";
            if (!string.IsNullOrWhiteSpace(dbName))
            {
                connStr += ";Database=" + dbName.Trim();
            }
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            return conn;
        }

        static void CheckName(string name, string title)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception(title + "是空的");
            }
            foreach (char c in name)
            {
                if (!(char.IsLetterOrDigit(c) || c == '_'))
                {
                    throw new Exception(title + "只能用字母、数字、下划线：" + name);
                }
            }
        }

        static string Quote(string name)
        {
            return "`" + name + "`";
        }
    }
}