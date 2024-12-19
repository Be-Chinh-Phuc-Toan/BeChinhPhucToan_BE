using System.ComponentModel.DataAnnotations;

namespace BeChinhPhucToan_BE.Models
{
    public class User : BaseEntity
    {
        [Key]
        public string phoneNumber { get; set; }
        public string? password { get; set; }
        public string? role { get; set; }
        public bool? isVerify { get; set; }
        public string? otpCode { get; set; }
        public string? otpExpiration { get; set; }       

        public Administrator? Administrator { get; set; }
        public Parent? Parent { get; set; }
        //public ICollection<Feedback>? Feedbacks { get; set; }
    }
}
