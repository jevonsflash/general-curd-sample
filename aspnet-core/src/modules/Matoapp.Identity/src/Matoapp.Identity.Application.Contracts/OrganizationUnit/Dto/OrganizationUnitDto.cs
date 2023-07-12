using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Matoapp.Identity.OrganizationUnit.Dto;

public class OrganizationUnitDto : ExtensibleFullAuditedEntityDto<Guid>, IMultiTenant
{

    public Guid? TenantId { get; set; }

    public string Code { get; set; }

    public string DisplayName { get; set; }
  
    public string Name => DisplayName;
 
    public ICollection<OrganizationUnitDto> Children { get; set; }

}
