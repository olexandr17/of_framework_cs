using System;
using System.IO;
using System.Net;
using System.Text;

namespace OFClassLibrary.Common.Helpers
{
    public static class RequestHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Get(string url, string data = null)
        {
            Uri uri = new Uri(url + (data == null ? "" : "?" + data));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            return GetResult((HttpWebResponse)request.GetResponse());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Post(string url, string data)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            var bytes = encoding.GetBytes(data);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Credentials = CredentialCache.DefaultCredentials;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
            }
            return GetResult((HttpWebResponse)request.GetResponse());
        }


        private static string GetResult(HttpWebResponse response)
        {
            var result = string.Empty;

            if (response != null)
            {
                var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = reader.ReadToEnd();
                reader.Close();
            }

            return result;
        }

    }
}
