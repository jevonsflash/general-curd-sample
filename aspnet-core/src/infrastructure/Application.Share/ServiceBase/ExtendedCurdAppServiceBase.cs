using System;
using System.Linq;
using System.Linq.Expressions;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Application.Share.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using System.Linq.Dynamic.Core;
using Volo.Abp.Auditing;
using Application.Share.Services;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Users;

namespace Application.Share.ServiceBase
{
    public abstract class ExtendedCurdAppServiceBase<TEntity, TEntityDto, TKey>
        : ExtendedCurdAppServiceBase<TEntity, TEntityDto, TKey, PagedAndSortedResultRequestDto>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        protected ExtendedCurdAppServiceBase(IRepository<TEntity, TKey> repository)
            : base(repository)
        {

        }
    }

    public abstract class ExtendedCurdAppServiceBase<TEntity, TEntityDto, TKey, TGetListInput>
        : ExtendedCurdAppServiceBase<TEntity, TEntityDto, TKey, TGetListInput, TEntityDto>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        protected ExtendedCurdAppServiceBase(IRepository<TEntity, TKey> repository)
            : base(repository)
        {

        }
    }


    public abstract class ExtendedCurdAppServiceBase<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput>
     : ExtendedCurdAppServiceBase<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>
     where TEntity : class, IEntity<TKey>
     where TEntityDto : IEntityDto<TKey>
    {
        protected ExtendedCurdAppServiceBase(IRepository<TEntity, TKey> repository)
            : base(repository)
        {

        }
    }

    public abstract class ExtendedCurdAppServiceBase<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    : ExtendedCurdAppServiceBase<TEntity, TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    where TEntity : class, IEntity<TKey>
    where TEntityDto : IEntityDto<TKey>
    {
        protected ExtendedCurdAppServiceBase(IRepository<TEntity, TKey> repository)
            : base(repository)
        {

        }

        protected override Task<TEntityDto> MapToGetListOutputDtoAsync(TEntity entity)
        {
            return MapToGetOutputDtoAsync(entity);
        }

        protected override TEntityDto MapToGetListOutputDto(TEntity entity)
        {
            return MapToGetOutputDto(entity);
        }
    }

    public abstract class ExtendedCurdAppServiceBase<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
: ExtendedCurdAppServiceBase<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TGetListInput, TCreateInput, TUpdateInput>
where TEntity : class, IEntity<TKey>
where TGetOutputDto : IEntityDto<TKey>
where TGetListOutputDto : IEntityDto<TKey>
    {
        protected ExtendedCurdAppServiceBase(IRepository<TEntity, TKey> repository)
            : base(repository)
        {

        }
    }


    public abstract class ExtendedCurdAppServiceBase<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TGetListBriefInput, TCreateInput, TUpdateInput>
: ExtendedCurdAppServiceBase<TEntity, TGetOutputDto, TGetListOutputDto, TGetListOutputDto, TKey, TGetListInput, TGetListBriefInput, TCreateInput, TUpdateInput>
where TEntity : class, IEntity<TKey>
where TGetOutputDto : IEntityDto<TKey>
where TGetListOutputDto : IEntityDto<TKey>
        where TGetListBriefInput : TGetListInput
    {
        protected ExtendedCurdAppServiceBase(IRepository<TEntity, TKey> repository)
            : base(repository)
        {

        }
    }


    public abstract class ExtendedCurdAppServiceBase<TEntity, TGetOutputDto, TGetListOutputDto, TGetListBriefOutputDto, TKey, TGetListInput, TGetListBriefInput, TCreateInput, TUpdateInput>
    : CrudAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
    where TEntity : class, IEntity<TKey>
        where TGetOutputDto : IEntityDto<TKey>
        where TGetListBriefInput : TGetListInput
where TGetListOutputDto : IEntityDto<TKey>
    {
        protected ExtendedCurdAppServiceBase(IRepository<TEntity, TKey> repository)
    : base(repository)
        {

        }

        private new Task<TGetOutputDto> UpdateAsync(TKey id, TUpdateInput input)
        {
            return base.UpdateAsync(id, input);
        }
        private new Task<PagedResultDto<TGetListOutputDto>> GetListAsync(TGetListInput input)
        {
            return base.GetListAsync(input);
        }

        public virtual async Task<TGetOutputDto> UpdateAsync(TUpdateInput input)
        {
            await CheckUpdatePolicyAsync();
            var entity = await GetEntityByIdAsync((input as IEntityDto<TKey>).Id);
            MapToEntity(input, entity);
            await Repository.UpdateAsync(entity, autoSave: true);
            return await MapToGetOutputDtoAsync(entity);

        }
        public virtual Task<PagedResultDto<TGetListOutputDto>> GetAllAsync(TGetListInput input)
        {
            return this.GetListAsync(input);
        }


        public async Task<PagedResultDto<TGetListBriefOutputDto>> GetAllBriefAsync(TGetListBriefInput input)
        {
            await CheckGetListPolicyAsync();
            var query = await CreateBriefFilteredQueryAsync(input);
            var totalCount = await AsyncExecuter.CountAsync(query);
            var entities = new List<TEntity>();
            var entityDtos = new List<TGetListBriefOutputDto>();
            if (totalCount > 0)
            {
                query = ApplySorting(query, input);
                query = ApplyPaging(query, input);
                entities = await AsyncExecuter.ToListAsync(query);
                entityDtos = ObjectMapper.Map<List<TEntity>, List<TGetListBriefOutputDto>>(entities);

            }

            return new PagedResultDto<TGetListBriefOutputDto>(
                totalCount,
                entityDtos
            );
        }

        public async Task<ListResultDto<TGetListBriefOutputDto>> GetAllBriefWithoutPageAsync(TGetListBriefInput input)
        {
            await CheckGetListPolicyAsync();
            var query = await CreateBriefFilteredQueryAsync(input);
            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);
            var entities = await AsyncExecuter.ToListAsync(query);
            var entityDtos = ObjectMapper.Map<List<TEntity>, List<TGetListBriefOutputDto>>(entities);
            return new ListResultDto<TGetListBriefOutputDto>(
                entityDtos
            );
        }


        protected virtual async Task<IQueryable<TEntity>> CreateBriefFilteredQueryAsync(TGetListBriefInput input)
        {
            var query = await ReadOnlyRepository.GetQueryableAsync();

            query = await DefaultConvention(input, query);

            return query;
        }



        protected override async Task<IQueryable<TEntity>> CreateFilteredQueryAsync(TGetListInput input)
        {
            var query = await ReadOnlyRepository.GetQueryableAsync();

            query = await DefaultConvention(input, query);

            return query;
        }

        protected virtual async Task<IQueryable<TEntity>> DefaultConvention(TGetListInput input, IQueryable<TEntity> query)
        {
            query = ApplySearchFiltered(query, input);
            query = ApplyUserOrientedFiltered(query, input);
            query = await ApplyOrganizationOrientedFiltered(query, input);
            query = await ApplyRelationToOrientedFiltered(query, input);
            query = await ApplyRelationFromOrientedFiltered(query, input);
            query = ApplyStartDateOrientedFiltered(query, input);
            query = ApplyEndDateOrientedFiltered(query, input);
            return query;
        }



        //Abstract Methods
        protected virtual Task<IEnumerable<Guid>> GetUserIdsByOrganizationAsync(Guid organizationUnitId)
        {
            return Task.FromResult((IEnumerable<Guid>)new List<Guid>());
        }

        protected virtual Task<IEnumerable<Guid>> GetUserIdsByRelatedToAsync(Guid userId, string relationType)
        {
            return Task.FromResult((IEnumerable<Guid>)new List<Guid>());
        }

        protected virtual Task<IEnumerable<Guid>> GetUserIdsByRelatedFromAsync(Guid userId, string relationType)
        {
            return Task.FromResult((IEnumerable<Guid>)new List<Guid>());
        }

        //Apply Filtered Methods

        protected virtual IQueryable<TEntity> ApplyUserOrientedFiltered(IQueryable<TEntity> query, TGetListInput input)
        {
            if (input is IUserOrientedFilter)
            {
                var filteredInput = input as IUserOrientedFilter;
                var entityUserIdIdiom = filteredInput.EntityUserIdIdiom;
                if (string.IsNullOrEmpty(entityUserIdIdiom))
                {
                    entityUserIdIdiom = "UserId";
                }
                if (HasProperty<TEntity>(entityUserIdIdiom))
                {
                    var property = typeof(TEntity).GetProperty(entityUserIdIdiom);
                    if (filteredInput != null && filteredInput.UserId.HasValue)
                    {
                        Guid userId = default;
                        if (filteredInput.UserId.Value == Guid.Empty)
                        {
                            using (var scope = ServiceProvider.CreateScope())
                            {
                                var currentUser = scope.ServiceProvider.GetRequiredService<ICurrentUser>();
                                if (currentUser != null)
                                {
                                    userId = currentUser.GetId();
                                }
                            }
                        }
                        else
                        {
                            userId = filteredInput.UserId.Value;
                        }

                        var parameter = Expression.Parameter(typeof(TEntity), "p");
                        var keyConstantExpression = Expression.Constant(userId, typeof(Guid));

                        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                        var expression = Expression.Equal(propertyAccess, keyConstantExpression);

                        var equalExpression = expression != null ?
                             Expression.Lambda<Func<TEntity, bool>>(expression, parameter)
                             : p => false;

                        query = query.Where(equalExpression);
                    }
                }
            }
            return query;
        }


        protected virtual async Task<IQueryable<TEntity>> ApplyOrganizationOrientedFiltered(IQueryable<TEntity> query, TGetListInput input)
        {
            if (input is IOrganizationOrientedFilter)
            {
                var filteredInput = input as IOrganizationOrientedFilter;
                var entityUserIdIdiom = filteredInput.EntityUserIdIdiom;
                if (string.IsNullOrEmpty(entityUserIdIdiom))
                {
                    entityUserIdIdiom = "UserId";
                }
                if (HasProperty<TEntity>(entityUserIdIdiom))
                {
                    var property = typeof(TEntity).GetProperty(entityUserIdIdiom);
                    if (filteredInput != null && filteredInput.OrganizationUnitId.HasValue)
                    {

                        var ids = await GetUserIdsByOrganizationAsync(filteredInput.OrganizationUnitId.Value);
                        Expression originalExpression = null;
                        var parameter = Expression.Parameter(typeof(TEntity), "p");
                        foreach (var id in ids)
                        {
                            var keyConstantExpression = Expression.Constant(id, typeof(Guid));
                            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                            var expressionSegment = Expression.Equal(propertyAccess, keyConstantExpression);

                            if (originalExpression == null)
                            {
                                originalExpression = expressionSegment;
                            }
                            else
                            {
                                originalExpression = Expression.Or(originalExpression, expressionSegment);
                            }
                        }

                        var equalExpression = originalExpression != null ?
                             Expression.Lambda<Func<TEntity, bool>>(originalExpression, parameter)
                             : p => false;

                        query = query.Where(equalExpression);
                    }
                }

            }
            return query;
        }

        protected virtual async Task<IQueryable<TEntity>> ApplyRelationToOrientedFiltered(IQueryable<TEntity> query, TGetListInput input)
        {
            if (input is IRelationToOrientedFilter)
            {
                var filteredInput = input as IRelationToOrientedFilter;
                var entityUserIdIdiom = filteredInput.EntityUserIdIdiom;
                if (string.IsNullOrEmpty(entityUserIdIdiom))
                {
                    entityUserIdIdiom = "UserId";
                }
                if (HasProperty<TEntity>(entityUserIdIdiom))
                {
                    var property = typeof(TEntity).GetProperty(entityUserIdIdiom);
                    if (filteredInput != null && filteredInput.RelationToUserId.HasValue && !string.IsNullOrEmpty(filteredInput.RelationType))
                    {

                        Guid userId = default;
                        if (filteredInput.RelationToUserId.Value == Guid.Empty)
                        {
                            using (var scope = ServiceProvider.CreateScope())
                            {
                                var currentUser = scope.ServiceProvider.GetRequiredService<ICurrentUser>();
                                if (currentUser != null)
                                {
                                    userId = currentUser.GetId();
                                }
                            }
                        }
                        else
                        {
                            userId = filteredInput.RelationToUserId.Value;
                        }

                        var ids = await GetUserIdsByRelatedToAsync(userId, filteredInput.RelationType);
                        Expression originalExpression = null;
                        var parameter = Expression.Parameter(typeof(TEntity), "p");
                        foreach (var id in ids)
                        {
                            var keyConstantExpression = Expression.Constant(id, typeof(Guid));
                            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                            var expressionSegment = Expression.Equal(propertyAccess, keyConstantExpression);

                            if (originalExpression == null)
                            {
                                originalExpression = expressionSegment;
                            }
                            else
                            {
                                originalExpression = Expression.Or(originalExpression, expressionSegment);
                            }
                        }

                        var equalExpression = originalExpression != null ?
                             Expression.Lambda<Func<TEntity, bool>>(originalExpression, parameter)
                             : p => false;

                        query = query.Where(equalExpression);

                    }

                }
            }
            return query;
        }


        protected virtual async Task<IQueryable<TEntity>> ApplyRelationFromOrientedFiltered(IQueryable<TEntity> query, TGetListInput input)
        {
            if (input is IRelationFromOrientedFilter)
            {
                var filteredInput = input as IRelationFromOrientedFilter;
                var entityUserIdIdiom = filteredInput.EntityUserIdIdiom;
                if (string.IsNullOrEmpty(entityUserIdIdiom))
                {
                    entityUserIdIdiom = "UserId";
                }
                if (HasProperty<TEntity>(entityUserIdIdiom))
                {
                    var property = typeof(TEntity).GetProperty(entityUserIdIdiom);
                    if (filteredInput != null && filteredInput.RelationFromUserId.HasValue && !string.IsNullOrEmpty(filteredInput.RelationType))
                    {

                        Guid userId = default;
                        if (filteredInput.RelationFromUserId.Value == Guid.Empty)
                        {
                            using (var scope = ServiceProvider.CreateScope())
                            {
                                var currentUser = scope.ServiceProvider.GetRequiredService<ICurrentUser>();
                                if (currentUser != null)
                                {
                                    userId = currentUser.GetId();
                                }
                            }
                        }
                        else
                        {
                            userId = filteredInput.RelationFromUserId.Value;
                        }

                        var ids = await GetUserIdsByRelatedFromAsync(userId, filteredInput.RelationType);
                        Expression originalExpression = null;
                        var parameter = Expression.Parameter(typeof(TEntity), "p");
                        foreach (var id in ids)
                        {
                            var keyConstantExpression = Expression.Constant(id, typeof(Guid));
                            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                            var expressionSegment = Expression.Equal(propertyAccess, keyConstantExpression);

                            if (originalExpression == null)
                            {
                                originalExpression = expressionSegment;
                            }
                            else
                            {
                                originalExpression = Expression.Or(originalExpression, expressionSegment);
                            }
                        }

                        var equalExpression = originalExpression != null ?
                             Expression.Lambda<Func<TEntity, bool>>(originalExpression, parameter)
                             : p => false;

                        query = query.Where(equalExpression);

                    }
                }
            }
            return query;
        }


        protected virtual IQueryable<TEntity> ApplyStartDateOrientedFiltered(IQueryable<TEntity> query, TGetListInput input)
        {
            if (input is IStartDateOrientedFilter && HasProperty<TEntity>("CreationTime"))
            {
                var property = typeof(TEntity).GetProperty("CreationTime");
                var filteredInput = input as IStartDateOrientedFilter;
                if (filteredInput != null && filteredInput.StartDate.HasValue)
                {
                    Expression originalExpression = null;
                    var parameter = Expression.Parameter(typeof(TEntity), "p");

                    var dateConstantExpression = Expression.Constant(filteredInput.StartDate.Value, typeof(DateTime));

                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var expression = Expression.GreaterThanOrEqual(propertyAccess, dateConstantExpression);

                    var equalExpression = expression != null ?
                         Expression.Lambda<Func<TEntity, bool>>(expression, parameter)
                         : p => false;


                    query = query.Where(equalExpression);

                }

            }
            return query;
        }

        protected virtual IQueryable<TEntity> ApplyEndDateOrientedFiltered(IQueryable<TEntity> query, TGetListInput input)
        {
            if (input is IEndDateOrientedFilter && HasProperty<TEntity>("CreationTime"))
            {
                var property = typeof(TEntity).GetProperty("CreationTime");
                var filteredInput = input as IEndDateOrientedFilter;
                if (filteredInput != null && filteredInput.EndDate.HasValue)
                {
                    Expression originalExpression = null;
                    var parameter = Expression.Parameter(typeof(TEntity), "p");

                    var dateConstantExpression = Expression.Constant(filteredInput.EndDate.Value, typeof(DateTime));

                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var expression = Expression.LessThan(propertyAccess, dateConstantExpression);

                    var equalExpression = expression != null ?
                         Expression.Lambda<Func<TEntity, bool>>(expression, parameter)
                         : p => false;


                    query = query.Where(equalExpression);

                }

            }
            return query;
        }


        protected virtual IQueryable<TEntity> ApplySearchFiltered(IQueryable<TEntity> query, TGetListInput input)
        {
            if (input is IKeywordOrientedFilter)
            {
                var filteredInput = input as IKeywordOrientedFilter;
                if (filteredInput != null)
                {
                    var targetFields = new string[] { "Name", "Title" };
                    if (!string.IsNullOrEmpty(filteredInput.TargetFields))
                    {
                        targetFields = filteredInput.TargetFields.Split(',');
                    }

                    return query.WhereIf(!filteredInput.Keyword.IsNullOrWhiteSpace(),
                        FilterByKeywordDynamic<TEntity>(filteredInput.Keyword, targetFields));
                }
            }
            return query;
        }

        private Expression<Func<TEntity, bool>> FilterByKeywordDynamic<T>(string keyword, params string[] sortColumns)
        {
            var parameter = Expression.Parameter(typeof(T), "p");
            var propertys = sortColumns.Select(sortColumn => typeof(T).GetProperty(sortColumn));

            var method = typeof(string)
                .GetMethods()
                .FirstOrDefault(x => x.Name == "Contains");

            var keyConstantExpression = Expression.Constant(keyword, typeof(string));
            Expression originalExpression = null;
            foreach (var property in propertys)
            {
                if (property != null)
                {
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var expression = Expression.Call(propertyAccess, method, keyConstantExpression);
                    if (originalExpression == null)
                    {
                        originalExpression = expression;
                    }
                    else
                    {
                        originalExpression = Expression.Or(originalExpression, expression);
                    }
                }
            }

            var result = originalExpression != null ?
                 Expression.Lambda<Func<TEntity, bool>>(originalExpression, parameter)
                 : p => true;
            return result;


        }

        private bool HasProperty<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName) != null;
        }

        private bool HasProperty(TEntity entity, string propertyName)
        {
            return entity.GetType().GetProperty(propertyName) != null;
        }


        private bool HasProperty(Type type, string propertyName)
        {
            return type.GetProperty(propertyName) != null;
        }



    }



}