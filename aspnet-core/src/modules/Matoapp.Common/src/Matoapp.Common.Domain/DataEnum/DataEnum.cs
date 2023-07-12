using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Matoapp.Common.DataEnum
{
    public class DataEnum : FullAuditedEntity<long>
    {
        public int? TenantId { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override long Id { get; protected set; }
        public long DataEnumCategoryId { get; set; }

        [ForeignKey("DataEnumCategoryId")]
        public DataEnumCategory.DataEnumCategory DataEnumCategory { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 系统配置
        /// </summary>
        public bool IsSys { get; set; }


    }
}
