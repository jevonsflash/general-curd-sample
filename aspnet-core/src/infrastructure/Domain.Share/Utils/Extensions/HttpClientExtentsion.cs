using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace Domain.Share.Utils.Extensions
{
    public static class HttpClientExtentsion
    {
        /// <summary>
        /// 拼接url，支持二级目录
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url">url</param>
        /// <param name="query">url参数</param>
        /// <returns></returns>
        public static string GetUrl(this HttpClient client, string url, object query = null)
        {
            var querys = new Dictionary<string, string>();
            if (query != null)
            {
                querys = query.GetType().GetRuntimeProperties().ToDictionary(p => p.Name, p => p.GetValue(query).ToString());
            }

            return client.GetUrl(url, querys);
        }

        /// <summary>
        /// 拼接url，支持二级目录
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url">url</param>
        /// <param name="query">url参数</param>
        /// <returns></returns>
        public static string GetUrl(this HttpClient client, string url, Dictionary<string, string> query)
        {
            if (query == null) query = new Dictionary<string, string>();

            var combineUrl = CombineUrl(url, query);

            if (url.StartsWith("http://") || url.StartsWith("https://")) return combineUrl;

            if (client.BaseAddress == null) return combineUrl;

            return $"{client.BaseAddress.AbsoluteUri.TrimEnd('/')}/{combineUrl.Trim('/')}";
        }

        private static string CombineUrl(string url, Dictionary<string, string> query = null)
        {
            if (query.IsNullOrEmpty()) return url;

            var queryString = query.Select(p => $"{p.Key}={p.Value}").JoinAsString("&");
            return $"{url}{(url.Contains("?") ? "&" : "?")}{queryString}";
        }
    }
}
