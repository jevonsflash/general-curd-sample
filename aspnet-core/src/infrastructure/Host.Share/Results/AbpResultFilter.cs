using System;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Volo.Abp.DependencyInjection;
using Host.Share.Results.Wrapping;
using System.Reflection;
using Volo.Abp.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Domain.Share.Web.Models;

namespace Host.Share.Results
{
    public class AbpResultFilter : IResultFilter, ITransientDependency
    {
        private readonly IAbpActionResultWrapperFactory _actionResultWrapperFactory;

        public AbpResultFilter(IAbpActionResultWrapperFactory actionResultWrapper)
        {
            _actionResultWrapperFactory = actionResultWrapper;
        }

        public virtual void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.ActionDescriptor is not ControllerActionDescriptor)
            {
                return;
            }
            var methodInfo = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo;

            var wrapResultAttribute =
                ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<WrapResultAttribute>(
                    methodInfo,
                    null
                ); ;

            if (wrapResultAttribute == null)
            {
                return;
            }

            _actionResultWrapperFactory.CreateFor(context).Wrap(context);
        }

        public virtual void OnResultExecuted(ResultExecutedContext context)
        {
            //no action
        }
    }
}
