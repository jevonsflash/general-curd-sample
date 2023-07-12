using System;

namespace Application.Share.Services
{
    public interface IRelationToOrientedFilter
    {
        Guid? RelationToUserId { get; set; }

        string RelationType { get; set; }

    }
}