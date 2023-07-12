using Matoapp.Identity.Relation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matoapp.Identity.Relation.Dto
{
    public class GetRelatedUsersInput : IValidatableObject
    {
        public Guid UserId { get; set; }
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
