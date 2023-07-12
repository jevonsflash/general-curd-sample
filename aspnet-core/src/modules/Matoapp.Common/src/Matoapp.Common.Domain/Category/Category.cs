using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Matoapp.Common.Category
{

    public abstract class Category : FullAuditedEntity<long>
    {
        public int? TenantId { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override long Id { get; protected set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }

        public string Style { get; set; }

        public long? ParentId { get; set; }

    }
}
