using System;
using System.ComponentModel.DataAnnotations;
using Application.Share.Dto;
using Volo.Abp.Application.Dtos;

namespace Matoapp.Identity.OrganizationUnit.Dto
{
    public class GetOrganizationUnitUsersInput : PagedAndSortedResultRequestDto
    {
        //keyword
        public string Keyword { get; set; }
        public Guid Id { get; set; }
        public string[] Type { get; set; }
    }
}