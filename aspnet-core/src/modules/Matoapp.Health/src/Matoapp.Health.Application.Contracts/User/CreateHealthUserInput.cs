using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace Matoapp.Health.User
{
    public abstract class CreateHealthUserInput : IdentityUserCreateDto
    {


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

        public double Age { get; set; }

        public string FullName { get; set; }

        public string FullDistrict { get; set; }


    }

}
