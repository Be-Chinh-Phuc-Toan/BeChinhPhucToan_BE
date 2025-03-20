using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BeChinhPhucToan_BE.Models
{
    public class UserNotification : BaseEntity
    {
        public int id { get; set; }
        [Column(TypeName = "TEXT")]
        public string message { get; set; }
        public bool isRead { get; set; }
    }
}
