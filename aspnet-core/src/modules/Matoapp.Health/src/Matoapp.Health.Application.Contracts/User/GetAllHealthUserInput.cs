using Application.Share.Dto;
using Application.Share.Services;
using System;
using Volo.Abp.Application.Dtos;

namespace Matoapp.Health.User
{
    public class GetAllHealthUserInput : PagedAndSortedResultRequestDto, IKeywordOrientedFilter
    {
        public Guid? OrganizationUnitId { get; set; }
        public bool IsWithoutOrganization { get; set; }
        public long[] TagIds { get; set; }
        public string Keyword { get; set; }
        public string TargetFields { get; set; }

        public GetAllHealthUserInput()
        {
            TagIds = new long[] { };
        }
    }

}