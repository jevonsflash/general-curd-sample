using Volo.Abp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Share.Services;

namespace Matoapp.Health.Alarm.Dto
{
    public class GetAllAlarmInput : PagedAndSortedResultRequestDto, IUserOrientedFilter, IOrganizationOrientedFilter, IRelationToOrientedFilter, IKeywordOrientedFilter, IDateSpanOrientedFilter
    {
        //keyword
        public string Keyword { get; set; }
        public string TargetFields { get; set; }

        public string Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Type { get; set; }

        //医生-患者视图相关
        public Guid? UserId { get; set; }
        public Guid? OrganizationUnitId { get; set; }
        public Guid? RelationToUserId { get ; set ; }
        public string RelationType { get; set; }
        public string EntityUserIdIdiom { get; }
    }
}
