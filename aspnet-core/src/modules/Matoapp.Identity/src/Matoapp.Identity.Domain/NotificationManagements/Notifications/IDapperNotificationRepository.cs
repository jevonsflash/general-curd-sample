using Matoapp.Identity.NotificationManagements.Notifications.Aggregates;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Matoapp.Identity.NotificationManagements.Notifications
{
    public interface IDapperNotificationRepository : ITransientDependency
    {
        /// <summary>
        /// 分页查询广播消息
        /// </summary>
        /// <returns></returns>
        Task<List<Notification>> GetBroadCastNotificationByUserIdAsync(
            Guid userId,
            int maxResultCount = 10,
            int skipCount = 0,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 获取广播消息总条数
        /// </summary>
        /// <returns></returns>
        Task<int> GetPageBroadCastNotificationCountByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 分页查询文本消息
        /// </summary>
        /// <returns></returns>
        Task<List<Notification>> GetTextNotificationByUserIdAsync(
            Guid userId,
            int maxResultCount = 10,
            int skipCount = 0,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取文本息总条数
        /// </summary>
        /// <returns></returns>
        Task<int> GetPageTextNotificationCountByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
    }
}