using Domain.Share.Utils.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Volo.Abp.DependencyInjection;

namespace Domain.Share.Utils.Helpers
{
    public class SignHelper : ISingletonDependency
    {
        private readonly ILogger<SignHelper> _logger;

        public SignHelper(ILogger<SignHelper> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// URL参数加签
        /// </summary>
        /// <param name="urlQuery">加签对象</param>
        /// <param name="signKey">加签值</param>
        /// <param name="ignoreKey">忽略键</param>
        /// <param name="sortKey">排序加签</param>
        /// <param name="separator">拼接项的分隔符</param>
        /// <param name="containKey">拼接项包含键</param>
        /// <param name="keyValueSeparator">拼接键值分隔符，只有containKey=true才有意义</param>
        /// <param name="isLowerKey">键是否小写</param>
        /// <param name="justCombine">仅拼接</param>
        /// <returns></returns>
        public string Sign(string urlQuery, string signKey, string ignoreKey = "", bool sortKey = true, string separator = "", bool containKey = false, string keyValueSeparator = "=", bool justCombine = false, bool isLowerKey = true)
        {
            //todo 注意测试a=
            var dic = urlQuery
                .Split('&')
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(p => p.Split('='))
                .ToDictionary(p => p.FirstOrDefault(), p => p.LastOrDefault());

            return Sign(dic, signKey, ignoreKey: ignoreKey, sortKey: sortKey, separator: separator, containKey: containKey, keyValueSeparator: keyValueSeparator, justCombine, isLowerKey);
        }

        /// <summary>
        /// 对象加签
        /// </summary>
        /// <param name="model">加签对象</param>
        /// <param name="signKey">加签值</param>
        /// <param name="ignoreKey">忽略键</param>
        /// <param name="sortKey">排序加签</param>
        /// <param name="separator">拼接项的分隔符</param>
        /// <param name="containKey">拼接项包含键</param>
        /// <param name="keyValueSeparator">拼接键值分隔符，只有containKey=true才有意义</param>
        /// <param name="isLowerKey">键是否小写</param>
        /// <param name="isSkipNullValue">跳过空值</param>
        /// <param name="justCombine">仅拼接</param>
        /// <returns></returns>
        public string Sign(object model, string signKey, string ignoreKey = "", bool sortKey = true, string separator = "", bool containKey = false, string keyValueSeparator = "=", bool justCombine = false, bool isSkipNullValue = false, bool isLowerKey = true)
        {
            var dic = Obj2Dic(model, sortKey, isSkipNullValue: isSkipNullValue);

            return Sign(dic, signKey, ignoreKey: ignoreKey, sortKey: sortKey, separator: separator, containKey: containKey, keyValueSeparator: keyValueSeparator, justCombine, isLowerKey);
        }

        /// <summary>
        /// 字典加签
        /// </summary>
        /// <param name="dic">加签对象</param>
        /// <param name="signKey">加签值</param>
        /// <param name="ignoreKey">忽略键</param>
        /// <param name="sortKey">排序加签</param>
        /// <param name="separator">拼接项的分隔符</param>
        /// <param name="containKey">拼接项包含键</param>
        /// <param name="keyValueSeparator">拼接键值分隔符，只有containKey=true才有意义</param>
        /// <param name="isLowerKey">键是否小写</param>
        /// <param name="justCombine">仅拼接</param>
        /// <returns></returns>
        public string Sign(Dictionary<string, string> dic, string signKey, string ignoreKey = "", bool sortKey = true, string separator = "", bool containKey = false, string keyValueSeparator = "=", bool justCombine = false, bool isLowerKey = true)
        {
            if (isLowerKey)
            {
                dic = dic.ToDictionary(p => p.Key.ToLower(), p => p.Value);
            }

            var list = dic.AsEnumerable();
            _logger.LogDebug($"字典加签内容：{list.ToJsonString()}");

            if (sortKey)
            {
                list = list.OrderBy(p => p.Key);
                _logger.LogDebug($"字典加签需要排序");
            }
            if (!string.IsNullOrWhiteSpace(ignoreKey))
            {
                list = list.Where(p => p.Key != ignoreKey.ToLower());
                _logger.LogDebug($"字典加签需要忽略键：{ignoreKey}");
            }

            var joins = list.Select(p => p.Value);
            if (containKey)
            {
                joins = list.Select(p => $"{p.Key}{keyValueSeparator}{p.Value}");
                _logger.LogDebug($"字典加签需要包含键名，使用：{keyValueSeparator}进行连接");
            }

            _logger.LogDebug($"字典加签经过处理内容：{list.ToJsonString()}");

            var encryptString = joins.JoinAsString(separator);
            if (justCombine)
            {
                _logger.LogDebug($"字典加签，使用拼接符：{separator}，最终拼接内容：{encryptString}");
                return encryptString;
            }

            encryptString += signKey;
            _logger.LogDebug($"字典加签，使用拼接符：{separator}，最终加密内容：{encryptString}");

            return MD5Helper.MD5(encryptString);
        }

        /// <summary>
        /// 加签
        /// </summary>
        /// <param name="privateKey">合作方公钥</param>
        /// <param name="encryptString">加签对象</param>
        /// <param name="rsaType"></param>
        /// <returns></returns>
        public string SignRSA(string privateKey, string encryptString, RSAType rsaType = RSAType.SHA1)
        {
            return new RSAHelper().Sign(encryptString, privateKey, type: rsaType);
        }

        /// <summary>
        /// 验签
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="encryptString"></param>
        /// <param name="sign"></param>
        /// <param name="rsaType"></param>
        /// <returns></returns>
        public bool Verify(string publicKey, string encryptString, string sign, RSAType rsaType = RSAType.SHA1)
        {
            return new RSAHelper().Verify(encryptString, sign, publicKey, type: rsaType);
        }

        /// <summary>
        /// 对象转字典
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sort"></param>
        /// <param name="keyFunc"></param>
        /// <param name="isSkipNullValue"></param>
        /// <returns></returns>
        public Dictionary<string, string> Obj2Dic(object model, bool sort = false, Func<string, object, string> keyFunc = null, bool isSkipNullValue = false)
        {
            if (model == null) return new Dictionary<string, string>();

            if (keyFunc == null)
            {
                keyFunc = (key, value) =>
                {
                    if (value == null) return null;

                    var valueType = value.GetType().GetUnderlyingType();

                    if (valueType.IsEnum()) return ((int)value).ToString();

                    if (valueType.IsArray()) return value.ToJsonString();

                    if (valueType == typeof(DateTime)) return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");

                    return value.ToString();
                };
            }

            var items = model
                .GetType()
                .GetRuntimeProperties()
                .Select(p => new KeyValuePair<string, string>(p.Name, keyFunc.Invoke(p.Name, p.GetValue(model))))
                .WhereIf(isSkipNullValue, p => p.Value.IsNotNull());
            if (sort)
            {
                items = items.OrderBy(p => p.Key);
            }

            return items.ToDictionary(p => p.Key, p => p.Value);
        }
    }
}
