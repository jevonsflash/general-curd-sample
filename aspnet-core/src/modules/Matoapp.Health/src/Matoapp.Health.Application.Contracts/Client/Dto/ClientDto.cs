using Matoapp.Health.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Users;

namespace Matoapp.Health.Client.Dto
{
    public class ClientDto : HealthUserDto
    {
        //unique
        [StringLength(12)]
        public string ClientNumber { get; set; }

        public string ClientNumberType { get; set; }
   
        [Range(0.0, 250.0)]
        public double? Height { get; set; }


        [Range(0.0, 1000.0)]
        public double? Weight { get; set; }

        public string Marriage { get; set; }

        public string Status { get; set; }
    }
}
