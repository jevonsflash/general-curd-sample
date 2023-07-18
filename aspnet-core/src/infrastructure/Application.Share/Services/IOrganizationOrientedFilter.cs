using System;

namespace Application.Share.Services
{
    public interface IOrganizationOrientedFilter
    {

        public string EntityUserIdIdiom { get; }
        Guid? OrganizationUnitId { get; set; }
    }
}