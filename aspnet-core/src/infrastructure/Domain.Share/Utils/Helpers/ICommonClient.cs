using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Share.Utils.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommonClient
    {
        /// <summary>
        /// 设置地址
        /// </summary>
        /// <param name="url"></param>
        void SetBaseAddress(string url);

        /// <summary>
        /// 设置地址
        /// </summary>
        /// <param name="uri"></param>
        void SetBaseAddress(Uri uri);

        /// <summary>
        /// 通用Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <param name="body"></param>
        /// <param name="header"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        Task<string> PostStringAsync(string url, object query = null, object body = null, object header = null, Encoding encoding = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <param name="body"></param>
        /// <param name="header"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        Task<T> PostAsync<T>(string url, object query = null, object body = null, object header = null, Encoding encoding = null) where T : class, new();

        /// <summary>
        /// 通用Get请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="query">请求参数,会自动拼接成<![CDATA[a=1&b=2]]></param>
        /// <returns></returns>
        Task<string> GetStringAsync(string url, object query = null);

        /// <summary>
        /// 通用Get请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="query">请求参数,会自动拼接成<![CDATA[a=1&b=2]]></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string url, object query = null) where T : class, new();

        /// <summary>
        /// 通用请求
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <param name="body"></param>
        /// <param name="encoding"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        Task<string> SendAsync(HttpMethod httpMethod, string url, object query = null, object body = null, object header = null, Encoding encoding = null);

        /// <summary>
        /// 通用请求
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <param name="body"></param>
        /// <param name="encoding"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        Task<string> SendAsync(HttpMethod httpMethod, string url, Dictionary<string, string> query = null, object body = null, Dictionary<string, string> header = null, Encoding encoding = null);

        /// <summary>
        /// 获取请求
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <param name="body"></param>
        /// <param name="encoding"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        Task<HttpRequestMessage> GetHttpRequestMessage(HttpMethod httpMethod, string url, object query = null, object body = null, object header = null, Encoding encoding = null);

        /// <summary>
        /// 获取请求
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <param name="body"></param>
        /// <param name="encoding"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        Task<HttpRequestMessage> GetHttpRequestMessage(HttpMethod httpMethod, string url, Dictionary<string, string> query = null, object body = null, Dictionary<string, string> header = null, Encoding encoding = null);

        /// <summary>
        /// 获取响应
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetHttpResponseMessage(HttpRequestMessage requestMessage);

        /// <summary>
        /// 读取内容
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        Task<string> ReadStringAsync(HttpResponseMessage responseMessage);

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filename"></param>
        /// <param name="httpMethod"></param>
        /// <param name="query"></param>
        /// <param name="body"></param>
        /// <param name="header"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        Task DownloadFileAsync(string url, string filename, HttpMethod httpMethod = null, Dictionary<string, string> query = null, object body = null, Dictionary<string, string> header = null, Encoding encoding = null);
    }
}
