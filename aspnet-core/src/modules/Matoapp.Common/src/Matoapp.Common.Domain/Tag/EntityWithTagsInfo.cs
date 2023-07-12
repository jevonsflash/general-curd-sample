using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Matoapp.Common.Tag
{
    public class EntityWithTagsInfo<TEntity> where TEntity : IEntity<long>
    {
        public TEntity Entity { get; set; }

        public IEnumerable<Tag> Tags { get; set; }

        public EntityWithTagsInfo(TEntity entity, IEnumerable<Tag> tags)
        {
            Entity = entity;
            Tags = tags;
        }

        public EntityWithTagsInfo(TEntity entity)
        {
            Entity = entity;
            Tags = new List<Tag>();
        }
    }
}
