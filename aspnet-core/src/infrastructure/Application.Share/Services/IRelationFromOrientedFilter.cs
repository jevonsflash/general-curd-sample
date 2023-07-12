using System;

namespace Application.Share.Services
{
    public interface IRelationFromOrientedFilter
    {
        Guid? RelationFromUserId { get; set; }
        string RelationType { get; set; }


    }
}