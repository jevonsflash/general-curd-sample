using System;

namespace Application.Share.Services
{
    public interface IOrganizationOrientedFilter
    {
        Guid? OrganizationUnitId { get; set; }
    }
}