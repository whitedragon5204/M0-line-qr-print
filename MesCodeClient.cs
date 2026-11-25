using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace PackLabelPrint
{
    public class FetchResult
    {
        public bool Ok;
        public string Code;
        public string Message;
        public string Raw;
    }

    public static class MesCodeClient
    {
        public static FetchResult FetchPack()
        {
            return FetchByType(1, "PACK");
        }

        public static FetchResult FetchCustomer()
        {
            return FetchByType(2, "客户码");
        }

        public static PairFetchResult FetchPair()
        {
            PairFetchResult pair = new PairFetchResult();
            pair.Pack = FetchPack();
            if (!pair.Pack.Ok)
            {
                pair.Message = pair.Pack.Message;
                return pair;
            }

            pair.Customer = FetchCustomer();
            if (!pair.Customer.Ok)
            {
                pair.Message = "PACK 已领到（" + pair.Pack.Code + "），客户码失败：" + pair.Customer.Message;
                return pair;
            }

            pair.Ok = true;
            pair.Message = "领码成功";
            return pair;
        }

        static FetchResult FetchByType(int type, string title)
        {
            FetchResult result = new FetchResult();
            string url = ConfigurationManager.AppSettings["GenerateUrl"];
            if (string.IsNullOrWhiteSpace(url))
            {
                result.Message = "App.config 缺少 GenerateUrl";
                return result;
            }

            string jsonBody = "{\"count\":1,\"type\":" + type + "}";
            string raw = PostJson(url, jsonBody, out string err);
            result.Raw = raw;
            if (!string.IsNullOrEmpty(err))
            {
                result.Message = err;
                return result;
            }
            if (string.IsNullOrWhiteSpace(raw))
            {
                result.Message = "取码接口没有返回内容";
                return result;
            }

            try
            {
                ApiResponse api = JsonConvert.DeserializeObject<ApiResponse>(raw);
                if (api == null || api.Data == null || api.Data.Count == 0)
                {
                    result.Message = "取码接口没有 Data";
                    return result;
                }
                string code = (api.Data[0] ?? "").Trim();
                if (string.IsNullOrEmpty(code) || string.Equals(code, "ERROR", StringComparison.OrdinalIgnoreCase))
                {
                    result.Message = "取码失败：" + code;
                    return result;
                }
                result.Ok = true;
                result.Code = code;
                result.Message = "领" + title + "成功";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = "解析取码结果失败：" + ex.Message;
                return result;
            }
        }

        static string PostJson(string url, string jsonBody, out string err)
        {
            err = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "*/*";
            request.ContentType = "application/json";
            request.Timeout = 15000;
            request.ReadWriteTimeout = 15000;

            byte[] data = Encoding.UTF8.GetBytes(jsonBody);
            request.ContentLength = data.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse errorResponse)
                {
                    using (Stream errorStream = errorResponse.GetResponseStream())
                    using (StreamReader reader = new StreamReader(errorStream, Encoding.UTF8))
                    {
                        string body = reader.ReadToEnd();
                        err = "取码接口失败：" + ex.Message + " " + body;
                        return body;
                    }
                }
                err = "取码接口失败：" + ex.Message;
                return null;
            }
        }
    }

    public class PairFetchResult
    {
        public bool Ok;
        public string Message;
        public FetchResult Pack = new FetchResult();
        public FetchResult Customer = new FetchResult();
    }

    public class LabelPair
    {
        public const string Sep = " | ";
        public string Pack;
        public string Customer;

        public string Display
        {
            get { return (Pack ?? "") + Sep + (Customer ?? ""); }
        }

        public static bool TryParse(string text, out LabelPair pair)
        {
            pair = new LabelPair();
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }
            int i = text.IndexOf(Sep, StringComparison.Ordinal);
            if (i < 0)
            {
                pair.Pack = text.Trim();
                return pair.Pack.Length > 0;
            }
            pair.Pack = text.Substring(0, i).Trim();
            pair.Customer = text.Substring(i + Sep.Length).Trim();
            return pair.Pack.Length > 0 && pair.Customer.Length > 0;
        }
    }

    public class ApiResponse
    {
        public bool Success { get; set; }
        public int Code { get; set; }
        public string Msg { get; set; }
        public List<string> Data { get; set; }
    }
}
