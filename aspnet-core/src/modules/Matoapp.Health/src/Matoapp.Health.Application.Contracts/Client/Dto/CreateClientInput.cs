using System;
using System.ComponentModel.DataAnnotations;
using Matoapp.Health.User;

namespace Matoapp.Health.Client.Dto
{
    public class CreateClientInput : HealthUserDto
    {

        //public ICollection<TagDto> Tags { get; set; }
        public Guid? OrganizationUnitId { get; set; }
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
