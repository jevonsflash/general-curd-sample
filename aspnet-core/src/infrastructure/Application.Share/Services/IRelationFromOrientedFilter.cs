using System;

namespace Application.Share.Services
{
    public interface IRelationFromOrientedFilter
    {
        Guid? RelationFromUserId { get; set; }
        public string EntityUserIdIdiom { get;  }
        string RelationType { get; set; }


    }
}