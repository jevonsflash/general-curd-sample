using System;

namespace Matoapp.Identity.NotificationManagements.Notifications.Dtos
{
    /// <summary>
    /// 标记已读
    /// </summary>
    public class MarkReadInput
    {
        /// <summary>
        /// 消息Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 接收人Id
        /// </summary>
        public Guid ReceiveId { get; set; }
    }
}