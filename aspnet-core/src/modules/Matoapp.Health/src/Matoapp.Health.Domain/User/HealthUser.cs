using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Linq;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Matoapp.Health.User
{
    public class HealthUser<TKey> : FullAuditedAggregateRoot<TKey>
    {
        public HealthUser()
        {

        }
        public HealthUser(TKey id) : base(id)
        {

        }

        public virtual DateTimeOffset? LockoutEnd { get; protected set; }

        public virtual bool LockoutEnabled { get; protected set; }

        public virtual Guid? TenantId { get; protected set; }

        public virtual string UserName { get; protected set; }

        public virtual string Email { get; protected set; }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual bool EmailConfirmed { get; protected set; }

        public virtual string PhoneNumber { get; protected set; }

        public virtual bool PhoneNumberConfirmed { get; protected set; }

        public string Type { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nationality { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChatNumber { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentificationNumber { get; set; }
        /// <summary>
        /// 个性化图片
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string AvatarUrl { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDay { get; set; }
        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 籍贯住址
        /// </summary>
        public string NativeAddress { get; set; }
        /// <summary>
        /// 最高学历
        /// </summary>
        public string Education { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string PoliticsStatus { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string Occupation { get; set; }

        public string IdentificationType { get; set; }

        public string Ext2 { get; set; }
        public string Ext3 { get; set; }

        [NotMapped]
        public double Age
        {
            get
            {
                if (BirthDay.HasValue)
                {
                    var birthdate = BirthDay.Value;
                    var now = DateTime.Now;
                    var age = now.Year - birthdate.Year;
                    if (now.Month < birthdate.Month || now.Month == birthdate.Month && now.Day < birthdate.Day)
                    {
                        age--;
                    }

                    return age < 0 ? 0 : age;
                }
                else
                {
                    return 0;
                }
            }
        }

        [NotMapped]
        public string FullName => $"{Surname}{Name}";

        [NotMapped]
        public string FullDistrict => $"{Province} {City} {District}";
    }

}
