using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;
using Domain.Share.Utils.Helpers;
using Matoapp.Identity.NotificationManagements.Notifications.MaxLengths;
using Matoapp.Identity.NotificationManagements.Notifications.DistributedEvents;
using Matoapp.Identity.NotificationManagements.Notifications.Enums;

namespace Matoapp.Identity.NotificationManagements.Notifications.Aggregates
{
    /// <summary>
    /// 消息通知 
    /// </summary>
    public partial class Notification : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        [Required]
        [StringLength(NotificationMaxLengths.Title)]
        public string Title { get; private set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [Required]
        [StringLength(NotificationMaxLengths.Content)]
        public string Content { get; private set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [Required]
        public MessageType MessageType { get; private set; }

        /// <summary>
        /// 发送人
        /// </summary>
        [Required]
        public Guid SenderId { get; private set; }

        /// <summary>
        /// 关联属性1:N 消息订阅者集合
        /// </summary>
        public List<NotificationSubscription> NotificationSubscriptions { get; private set; }

        private Notification()
        {
            NotificationSubscriptions = new List<NotificationSubscription>();
        }

        public Notification(
            Guid id,
            string title,
            string content,
            MessageType messageType,
            Guid senderId
        ) : base(id)
        {
            NotificationSubscriptions = new List<NotificationSubscription>();

            SetProperties(
                title,
                content,
                messageType,
                senderId
            );
        }

        private void SetProperties(
            string title,
            string content,
            MessageType messageType,
            Guid senderId
        )
        {
            SetTitle(title);
            SetContent(content);
            SetMessageType(messageType);
            SetSenderId(senderId);
        }

        private void SetSenderId(Guid senderId)
        {
            CheckHelper.IsNotNull(senderId, nameof(senderId));
            SenderId = senderId;
        }

        private void SetTitle(string title)
        {
            CheckHelper.IsNotNull(title, nameof(title));
            Title = title;
        }

        private void SetContent(string content)
        {
            CheckHelper.IsNotNull(content, nameof(content));
            Content = content;
        }

        private void SetMessageType(MessageType messageType)
        {
            MessageType = messageType;
        }

        /// <summary>
        /// 新增非广播消息订阅人
        /// </summary>
        /// <param name="notificationSubscriptionId"></param>
        /// <param name="receiveId"></param>
        public void AddNotificationSubscription(Guid notificationSubscriptionId, Guid receiveId)
        {
            if (NotificationSubscriptions.Any(e => e.ReceiveId == receiveId)) return;
            NotificationSubscriptions.Add(new NotificationSubscription(notificationSubscriptionId, receiveId));
        }

        /// <summary>
        /// 新增消息类型为广播订阅人
        /// </summary>
        /// <param name="notificationSubscriptionId"></param>
        /// <param name="receiveId"></param>
        public void AddBroadCastNotificationSubscription(Guid notificationSubscriptionId,
            Guid receiveId)
        {
            if (NotificationSubscriptions.Any(e => e.ReceiveId == receiveId))
            {
                return;
            }
            else
            {
                var temp = new NotificationSubscription(notificationSubscriptionId, receiveId);
                temp.MarkRead();
                NotificationSubscriptions.Add(temp);
            }
        }

        /// <summary>
        /// 添加创建消息集成事件
        /// </summary>
        /// <param name="createdNotificationDistributedEvent"></param>
        public void AddCreatedNotificationDistributedEvent(
            CreatedNotificationDistributedEvent createdNotificationDistributedEvent)
        {
            AddDistributedEvent(createdNotificationDistributedEvent);
        }
    }
}