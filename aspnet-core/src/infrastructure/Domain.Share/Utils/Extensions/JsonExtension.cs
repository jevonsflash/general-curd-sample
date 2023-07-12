using Newtonsoft.Json;

namespace Domain.Share.Utils.Extensions
{
    /// <summary>
    /// json扩展
    /// </summary>
    public static class JsonExtension
    {
        /// <summary>
        /// 序列化设置
        /// </summary>
        public static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DateFormatString = "YYYY-MM-dd HH:mm:ss",
        };

        /// <summary>
        /// 对象序列化为Json字符
        /// </summary>
        /// <param name="model"></param>
        /// <param name="settings"></param>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public static string ToJsonString<TModel>(this TModel model, JsonSerializerSettings settings = null)
        {
            if (model == null) return string.Empty;

            if (model is string)
            {
                return model.ToString();
            }

            return JsonConvert.SerializeObject(model, settings ?? JsonSerializerSettings);
        }

        /// <summary>
        /// 将obj对象通过json反序列化转换为目标对象
        /// </summary>
        /// <param name="model"></param>
        /// <param name="settings"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T AsObject<T>(this object model, JsonSerializerSettings settings = null)
        {
            if (model == null) return default;

            var json = string.Empty;
            if (model is string)
            {
                json = model.ToString();
            }
            else
            {
                json = model.ToJsonString();
            }

            return JsonConvert.DeserializeObject<T>(json, settings ?? JsonSerializerSettings);
        }

        /// <summary>
        /// Json字符反序列化为对象
        /// </summary>
        /// <typeparam name="TModel">对象类型</typeparam>
        /// <param name="model"></param>
        /// <param name="anonymousTypeObject">目标类型数据模型</param>
        /// <param name="settings">序列化设定</param>
        /// <returns></returns>
        public static TModel AsAnonymousObject<TModel>(this object model, TModel anonymousTypeObject, JsonSerializerSettings settings = null) where TModel : class
        {
            var str = string.Empty;

            if (model == null) str = "{}";


            if (model is string)
            {
                str = model.ToString();
            }
            else
            {
                str = model.ToJsonString();
            }


            return JsonConvert.DeserializeAnonymousType(str, anonymousTypeObject, settings ?? JsonSerializerSettings);
        }
    }
}
