using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Matoapp.Identity.Relation.Dto
{
    public class ModifyRelationInput :  IValidatableObject
    {
        public Guid UserId { get; set; }

        public Guid RelatedUserId { get; set; }

        public string Type { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Type != RelationTypeConst.FollowTypeName && Type != RelationTypeConst.AttachTypeName)
            {
                yield return new ValidationResult("关系类型不合法!",
                      new[] { nameof(Type) });

            }

        }

    }
}
