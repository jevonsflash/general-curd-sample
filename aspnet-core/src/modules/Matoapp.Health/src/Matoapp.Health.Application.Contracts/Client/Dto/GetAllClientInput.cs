using Application.Share.Dto;
using Application.Share.Services;
using Matoapp.Health.User;
using System;
using Volo.Abp.Application.Dtos;

namespace Matoapp.Health.Client.Dto
{
    public class GetAllClientInput : GetAllHealthUserInput, IRelationToOrientedFilter
    {
        public Guid? RelationToUserId { get; set; }
        public string RelationType { get; set; }
        public string EntityUserIdIdiom { get; }
    }

}