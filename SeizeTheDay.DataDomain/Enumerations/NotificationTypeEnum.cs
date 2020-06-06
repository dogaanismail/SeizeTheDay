using System.ComponentModel.DataAnnotations;

namespace SeizeTheDay.DataDomain.Enumerations
{
    public enum NotificationTypeEnum
    {
        [Display(Name = "Notification")]
        Notification = 0,

        [Display(Name = "MessageNotification")]
        MessageNotification = 1,
    }
}
