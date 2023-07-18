using System;

namespace Application.Share.Services
{
    public interface IRelationToOrientedFilter
    {
        Guid? RelationToUserId { get; set; }
        public string EntityUserIdIdiom { get; }

        string RelationType { get; set; }

    }
}