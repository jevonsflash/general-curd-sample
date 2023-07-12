using Domain.Share.Utils.Extensions;
using System.Collections;
using Volo.Abp;

namespace Domain.Share.Utils.Helpers
{
    public class CheckHelper
    {
        public static void IsNull(object obj, string message = "", string name = "")
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new UserFriendlyException($"${nameof(name)}、{nameof(message)}必须提供一项");
                }

                message = $"{name}需要为空";
            }
            if (obj == null) return;

            var objType = obj.GetType();

            if (objType == typeof(string) && string.IsNullOrWhiteSpace(obj.ToString()) == false)
            {
                throw new UserFriendlyException(message);
            }

            if (objType.IsArray())
            {
                if (((ICollection)obj).Count > 0) throw new UserFriendlyException(message);
            }
        }

        public static void IsNotNull(object obj, string message = "", string name = "")
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new UserFriendlyException($"${nameof(name)}、{nameof(message)}必须提供一项");
                }

                message = $"{name}不能为空";
            }
            if (obj == null) throw new UserFriendlyException(message);

            var objType = obj.GetType();

            if (objType == typeof(string) && string.IsNullOrWhiteSpace(obj.ToString()))
            {
                throw new UserFriendlyException(message);
            }

            if (objType.IsArray())
            {
                if (((ICollection)obj).Count <= 0) throw new UserFriendlyException(message);
            }
        }

        public static void IsTrue(bool condition, string message = "")
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = $"条件为真";
            }
            if (condition != true) throw new UserFriendlyException(message);
        }

        public static void IsFalse(bool condition, string message = "")
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = $"条件为假";
            }
            if (condition != false) throw new UserFriendlyException(message);
        }

        public static void AreEqual<T>(T t1, T t2, string t1Name = "", string t2Name = "", string message = "")
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                if (string.IsNullOrWhiteSpace(t1Name) && string.IsNullOrWhiteSpace(t2Name))
                {
                    throw new UserFriendlyException($"name、{nameof(message)}必须提供一项");
                }

                if (string.IsNullOrWhiteSpace(t1Name))
                {
                    throw new UserFriendlyException($"请提供{nameof(t1Name)}参数");
                }

                if (string.IsNullOrWhiteSpace(t2Name))
                {
                    throw new UserFriendlyException($"请提供{nameof(t2Name)}参数");
                }

                message = $"{t1Name}和{t2Name}的值需要相等";
            }
            if (!t1.Equals(t2)) throw new UserFriendlyException(message);
        }

        public static void AreNotEqual<T>(T t1, T t2, string t1Name = "", string t2Name = "", string message = "")
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                if (string.IsNullOrWhiteSpace(t1Name) && string.IsNullOrWhiteSpace(t2Name))
                {
                    throw new UserFriendlyException($"name、{nameof(message)}必须提供一项");
                }

                if (string.IsNullOrWhiteSpace(t1Name))
                {
                    throw new UserFriendlyException($"请提供{nameof(t1Name)}参数");
                }

                if (string.IsNullOrWhiteSpace(t2Name))
                {
                    throw new UserFriendlyException($"请提供{nameof(t2Name)}参数");
                }

                message = $"{t1Name}和{t2Name}的值不能相等";
            }
            if (t1.Equals(t2)) throw new UserFriendlyException(message);
        }
    }
}

