using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Matoapp.Common.Tag
{
    public class EntityWithTagInfo<TEntity> where TEntity : IEntity<long>
    {
        public TEntity Entity { get; set; }

        public Tag Tag { get; set; }

        public EntityWithTagInfo(TEntity entity, Tag tag)
        {
            Entity = entity;
            Tag = tag;
        }

        public EntityWithTagInfo(TEntity entity)
        {
            Entity = entity;
        }
    }
}
