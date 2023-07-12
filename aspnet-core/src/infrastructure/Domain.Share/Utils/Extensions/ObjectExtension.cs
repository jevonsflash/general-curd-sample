using Domain.Share.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Share.Utils.Extensions
{
    /// <summary>
    /// Object扩展
    /// </summary>
    public static class ObjectExtension
    {
        public static T To<T>(this object obj, T defaultValue) where T : struct
        {
            try
            {
                T value = obj.To<T>();
                return value;
            }
            catch { }

            return defaultValue;
        }

        public static int To(this decimal obj, MidpointRounding mode)
        {
            return Math.Round(obj, 0, mode).To<int>();
        }

        public static int To(this double obj, MidpointRounding mode)
        {
            return Math.Round(obj, 0, mode).To<int>();
        }

        public static bool IsNull(this object obj)
        {
            return !obj.IsNotNull();
        }

        public static bool IsNotNull(this object obj)
        {
            if (obj == null) return false;

            var objType = obj.GetType();

            if (objType.IsArray
                || objType.Name == typeof(List<>).Name
                || objType.Name == typeof(IEnumerable<>).Name
                || objType.Name == typeof(Dictionary<,>).Name)
            {
                var anyMehtod = typeof(Enumerable).GetMethods().Where(p => p.Name == "Any").Where(p => p.GetParameters().Length == 1).FirstOrDefault();
                if (anyMehtod == null) return false;

                return (bool)anyMehtod.MakeGenericMethod(objType.GetGenericArguments()).Invoke(null, new object[] { obj });
            }

            return true;
        }

        /// <summary>
        /// 获取对象字段值
        /// </summary>
        /// <param name="model"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <returns></returns>
        public static TProperty GetPropValue<TModel, TProperty>(this TModel model, string name, TProperty defaultValue = default)
        {
            if (model == null) return defaultValue;

            var property = model.GetType().GetProperties().Where(p => p.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (property == null) return defaultValue;

            var propertyValue = property.GetValue(model);
            if (propertyValue == null) return defaultValue;

            return (TProperty)propertyValue;
        }

        /// <summary>
        /// 获取属性值，对象为null、属性为null返回指定值
        /// </summary>
        /// <param name="model"></param>
        /// <param name="expression"></param>
        /// <param name="defaultValue">指定值</param>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <returns></returns>
        public static TProperty GetPropValue<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression, TProperty defaultValue = default)
        {
            if (model == null) return defaultValue;

            var memberExpression = expression.Body as MemberExpression;
            var property = model.GetType().GetProperty(memberExpression.Member.Name);

            var propertyValue = property.GetValue(model);
            if (propertyValue == null) return defaultValue;

            return (TProperty)propertyValue;
        }

        /// <summary>
        /// 对象字段赋值
        /// </summary>
        /// <param name="model"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <returns></returns>
        public static TModel SetPropValue<TModel, TProperty>(this TModel model, string name, TProperty value)
        {
            if (model == null) return model;

            var property = model.GetType().GetProperties().Where(p => p.Name.ToLower() == name.ToLower()).FirstOrDefault();
            if (property != null)
            {
                property.SetValue(model, value);
            }

            return model;
        }

        /// <summary>
        /// 对象字段赋值
        /// </summary>
        /// <param name="model"></param>
        /// <param name="expression"></param>
        /// <param name="value"></param>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <returns></returns>
        public static TModel SetPropValue<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression, TProperty value)
        {
            if (model == null) return model;

            var memberExpression = expression.Body as MemberExpression;
            var property = model.GetType().GetProperty(memberExpression.Member.Name);

            if (property != null)
            {
                property.SetValue(model, value);
            }

            return model;
        }
    }
}
