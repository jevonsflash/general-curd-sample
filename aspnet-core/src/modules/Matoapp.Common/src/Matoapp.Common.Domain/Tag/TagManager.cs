using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace Matoapp.Common.Tag
{
    public class TagManagerForLong<T> : DomainService where T : TagConfig.TagConfigForLong, new()
    {
        private readonly IUnitOfWorkManager unitOfWorkManager;
        private readonly IRepository<Tag, long> _tagRepository;
        private readonly IRepository<T, long> _entityRepository;

        public TagManagerForLong(
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<Tag, long> tagRepository,
            IRepository<T, long> entityRepository
            )
        {
            this.unitOfWorkManager = unitOfWorkManager;
            _tagRepository = tagRepository;
            _entityRepository = entityRepository;
        }
        protected IUnitOfWork CurrentUnitOfWork => unitOfWorkManager?.Current;

        public IRepository<Tag, long> TagRepository => _tagRepository;
        public IRepository<T, long> EntityRepository => _entityRepository;

        public async Task<Tag> GetOrCreateIfNullTag(Tag inputTag)
        {
            Tag currentGroup;
            if (inputTag.Id != default)
            {
                currentGroup = await _tagRepository.FirstOrDefaultAsync(c => c.Id == inputTag.Id);
            }
            else
            {
                currentGroup = await _tagRepository.FirstOrDefaultAsync(c => c.Title == inputTag.Title);
                if (currentGroup == null)
                {
                    currentGroup =
                        await _tagRepository.InsertAsync(inputTag);
                }
            }

            return currentGroup;
        }

        public async Task Connect(List<Tag> tags, long id)
        {
            foreach (var inputClientTag in tags)
            {
                var currentGroup = await GetOrCreateIfNullTag(inputClientTag);
                await CurrentUnitOfWork.SaveChangesAsync();

                if (currentGroup != null)
                {

                    var model = new T()
                    {
                        EntityId = id,
                        TagId = currentGroup.Id
                    };
                    await _entityRepository.InsertAsync(model);
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task Disconnect(long id)
        {
            await _entityRepository.DeleteAsync(c => c.EntityId == id);

        }

        [UnitOfWork]
        public async Task<IEnumerable<TEntity>> GetEntitiesByTags<TEntity>(IEnumerable<TEntity> entities, long[] tagIds) where TEntity : IEntity<long>
        {
            var query2 = from client in entities
                         join ct in (await _entityRepository.GetQueryableAsync()) on client.Id equals ct.EntityId into temp
                         from a in temp.DefaultIfEmpty()
                         where tagIds == null || tagIds.Length == 0
                                || tagIds.Contains(a.TagId)
                         select client;

            return query2;
        }

   

        [UnitOfWork]
        public async Task<IEnumerable<EntityWithTagsInfo<TEntity>>> GetEntitiesWithTagsByTagsAsync<TEntity>(IEnumerable<TEntity> entities, long[] tagIds) where TEntity : IEntity<long>
        {
            var query2 = from p in
                             from client in entities
                             join ct in await _entityRepository.GetQueryableAsync() on client.Id equals ct.EntityId into temp
                             from a in temp.DefaultIfEmpty()
                             join tag in await _tagRepository.GetQueryableAsync() on a.TagId equals tag.Id

                             where tagIds == null || tagIds.Length == 0
                                    || tagIds.Contains(a.TagId)
                             select new { client, tag }
                         group p by p.client.Id into pt
                         select new EntityWithTagsInfo<TEntity>(pt.FirstOrDefault(c => c.client.Id == pt.Key).client, pt.Select(c => c.tag));

            return query2.ToList();
        }

       

        [UnitOfWork]
        public async Task<EntityWithTagsInfo<TEntity>> GetEntityWithTagsByTagsAsync<TEntity>(IEnumerable<TEntity> entities, long id) where TEntity : IEntity<long>
        {
            EntityWithTagsInfo<TEntity> result = default;
            var client = entities.FirstOrDefault(c => c.Id == id);

            var tags = (from cts in await _entityRepository.GetQueryableAsync()
                        join ts in await _tagRepository.GetQueryableAsync() on cts.TagId equals ts.Id into temp
                        from tst in temp.DefaultIfEmpty()
                        where cts.EntityId == client.Id
                        select tst).ToList();

            result = new EntityWithTagsInfo<TEntity>(client, tags);
            return result;
        }


        [UnitOfWork]
        public async Task<IEnumerable<EntityWithTagsInfo<TEntity>>> GetEntitiesWithTagsAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : IEntity<long>
        {

            var result = new List<EntityWithTagsInfo<TEntity>>();
            foreach (var client in entities.ToList())
            {
                var tags = (from cts in await _entityRepository.GetQueryableAsync()
                            join ts in await _tagRepository.GetQueryableAsync() on cts.TagId equals ts.Id into temp
                            from tst in temp.DefaultIfEmpty()
                            where cts.EntityId == client.Id
                            select tst).ToList();

                result.Add(new EntityWithTagsInfo<TEntity>(client, tags));
            }

            return result;

        }

    }
}
