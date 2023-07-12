using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matoapp.Identity.NotificationManagements.Hubs;
using Matoapp.Identity.NotificationManagements.Notifications.Dtos;
using Microsoft.AspNetCore.SignalR;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;
using Matoapp.Identity.NotificationManagements.Notifications.Aggregates;

namespace Matoapp.Identity.NotificationManagements.Notifications
{
    /// <summary>
    /// SignalR消息通知
    /// </summary>
    public class NotificationAppService : IdentityAppService, INotificationAppService
    {
        private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;
        private readonly NotificationManager _notificationManager;
        private readonly ICurrentUser _currentUser;
        private readonly IDapperNotificationRepository _dapperNotificationRepository;
        public NotificationAppService(
            IHubContext<NotificationHub, INotificationHub> hubContext,
            NotificationManager notificationManager,
            ICurrentUser currentUser,
            IDapperNotificationRepository dapperNotificationRepository)
        {
            _hubContext = hubContext;
            _notificationManager = notificationManager;
            _currentUser = currentUser;
            _dapperNotificationRepository = dapperNotificationRepository;
        }

        /// <summary>
        /// 标记已读
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task MarkReadAsync(MarkReadInput input)
        {
            return _notificationManager.MarkReadAsync(input.Id, input.ReceiveId);
        }

        /// <summary>
        /// 新增消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateAsync(CreateNotificationInput input)
        {
            if (_currentUser.IsAuthenticated)
            {
                await _notificationManager.CreateAsync(input.Title, input.Content, _currentUser.Id.Value, input.ReceiveIds, input.MessageType);
            }
        }


        /// <summary>
        /// 分页获取用户普通文本消息
        /// </summary>
        /// <param name="listInput"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<NotificationDto>> GetTextNotificationByUserIdAsync(NotificationListInput listInput)
        {
            if (!_currentUser.Id.HasValue)
            {
                return null;
            }

            var totalCount = await _dapperNotificationRepository.GetPageTextNotificationCountByUserIdAsync(_currentUser.Id.Value);

            var list = await _dapperNotificationRepository.GetTextNotificationByUserIdAsync(_currentUser.Id.Value, listInput.MaxResultCount, listInput.SkipCount);

            var result = ObjectMapper.Map<List<Notification>, List<NotificationDto>>(list);

            return new PagedResultDto<NotificationDto>(totalCount, result);
        }

        /// <summary>
        /// 分页获取广播消息
        /// </summary>
        /// <param name="listInput"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<NotificationDto>> GetBroadCastNotificationByUserIdAsync(NotificationListInput listInput)
        {
            if (!_currentUser.Id.HasValue)
            {
                return null;
            }

            var totalCount = await _dapperNotificationRepository.GetPageBroadCastNotificationCountByUserIdAsync(_currentUser.Id.Value);

            var list = await _dapperNotificationRepository.GetBroadCastNotificationByUserIdAsync(_currentUser.Id.Value, listInput.MaxResultCount, listInput.SkipCount);

            var result = ObjectMapper.Map<List<Notification>, List<NotificationDto>>(list);

            return new PagedResultDto<NotificationDto>(totalCount, result);
        }

        /// <summary>
        /// 发送消息指定客户端用户
        /// </summary>
        /// <param name="sendNotificationDto"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public async Task SendMessageToClientByUserIdAsync(SendNotificationToUserDto sendNotificationToUserDto)
        {
            var users = sendNotificationToUserDto.Users;
            if (users is { Count: > 0 })
            {
                await _hubContext.Clients
                    .Users(users.AsReadOnly().ToList())
                    .ReceiveTextMessageAsync(sendNotificationToUserDto);
            }
        }

        /// <summary>
        /// 发送消息到所有客户端
        /// 广播消息
        /// </summary>
        /// <param name="sendNotificationDto"></param>
        public async Task SendMessageToAllClientAsync(SendNotificationDto sendNotificationDto)
        {
            await _hubContext.Clients.All.ReceiveBroadCastMessageAsync(sendNotificationDto);
        }
    }
}