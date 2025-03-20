using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeChinhPhucToan_BE.Models
{
    [PrimaryKey(nameof(userId), nameof(notificationID))]
    public class NotifyUser : BaseEntity
    {
        [Column(TypeName = "TEXT")]
        public string userId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(UserNotification))]
        public int notificationID { get; set; }
        public UserNotification? UserNotification { get; set; }
    }
}
