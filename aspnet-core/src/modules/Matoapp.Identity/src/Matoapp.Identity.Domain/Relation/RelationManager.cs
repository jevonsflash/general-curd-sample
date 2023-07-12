
using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;


namespace Matoapp.Identity.Relation
{

    public class RelationManager : DomainService
    {
        private readonly IRepository<Relation> Repository;

        public RelationManager(
            IRepository<Relation> _repository)
        {
            Repository = _repository;


        }


        public async Task<List<Relation>> GetRelatedToUsersWidthDetailAsync(Guid userId, string type)
        {
            var query = (await Repository.WithDetailsAsync(
                              c => c.User,
                              c => c.RelatedUser))
                .WhereIf(userId != null, c => userId == c.UserId)
                .WhereIf(!string.IsNullOrEmpty(type), c => c.Type == type);
            var items = query.ToList();
            return items;

        }

        public async Task<List<Relation>> GetRelatedFromUsersWidthDetailAsync(Guid userId, string type)
        {
            var query = (await Repository.WithDetailsAsync(
                              c => c.User,
                              c => c.RelatedUser))
                .Where(c => userId == c.RelatedUserId)
                .WhereIf(!string.IsNullOrEmpty(type), c => c.Type == type);
            var items = query.ToList();
            return items;
        }

        public async Task<List<Relation>> GetRelatedToUsersAsync(Guid userId, string type)
        {
            var query = (await Repository.GetQueryableAsync())
                .WhereIf(userId != null, c => userId == c.UserId)
                .WhereIf(!string.IsNullOrEmpty(type), c => c.Type == type);
            var items = query.ToList();
            return items;

        }

        public async Task<List<Relation>> GetRelatedFromUsersAsync(Guid userId, string type)
        {
            var query = (await Repository.GetQueryableAsync())
                .Where(c => userId == c.RelatedUserId)
                .WhereIf(!string.IsNullOrEmpty(type), c => c.Type == type);
            var items = query.ToList();
            return items;
        }




        public async Task<Relation> CreateAsync(Guid userId, Guid relatedUserId, string type)
        {
            return await Repository.InsertAsync(new Relation()
            {
                RelatedUserId = relatedUserId,
                UserId = userId,
                Type = type
            });

        }

        public async Task ClearAllRelatedToUsersAsync(Guid userId, string type)
        {
            await Repository.DeleteAsync(c => userId == c.UserId && (!string.IsNullOrEmpty(type) ? c.Type == type : true));
        }

        public async Task ClearAllRelatedFromUsersAsync(Guid userId, string type)
        {
            await Repository.DeleteAsync(c => userId == c.RelatedUserId && (!string.IsNullOrEmpty(type) ? c.Type == type : true));
        }


        public async Task DeleteByUserId(Guid userId, Guid relatedUserId, string type)
        {
            await Repository.DeleteAsync(c => userId == c.UserId && c.RelatedUserId == relatedUserId && (!string.IsNullOrEmpty(type) ? c.Type == type : true));
        }


    }


}
