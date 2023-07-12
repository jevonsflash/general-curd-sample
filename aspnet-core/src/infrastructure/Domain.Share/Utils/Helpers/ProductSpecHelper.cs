using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Share.Utils.Helpers
{
    public class ProductSpecHelper
    {
        /// <summary>
        /// 规格转字典
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ToDic(string spec)
        {
            if (spec.IsNullOrWhiteSpace()) return new Dictionary<string, string>();

            return spec
                .Split("|", StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Split(':'))
                .ToDictionary(p => p.First(), p => p.LastOrDefault());
        }

        /// <summary>
        /// 是否包含规格
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="containSpec"></param>
        /// <returns></returns>
        public static bool IsContainSpec(string spec, string containSpec)
        {
            var specDic = ToDic(spec);
            var containSpecDic = ToDic(containSpec);

            return containSpecDic.Any(p => specDic.Any(s => s.Key == p.Key && s.Value == p.Value));
        }

        /// <summary>
        /// 是否包含规格
        /// </summary>
        /// <param name="specs"></param>
        /// <param name="containSpec"></param>
        /// <returns></returns>
        public static bool IsContainSpec(IEnumerable<string> specs, string containSpec)
        {
            return specs.Any(p => IsContainSpec(p, containSpec));
        }
    }
}
