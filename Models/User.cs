using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeChinhPhucToan_BE.Models
{
    public class User : IdentityUser
    {
        [Column(TypeName = "TEXT")]
        public string? FullName { get; set; }
        [Column(TypeName = "TEXT")]
        public string? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        [Column(TypeName = "TEXT")]
        public string? Address { get; set; }
        [Column(TypeName = "TEXT")]
        public string? Guardian { get; set; }
        public bool? IsVerify { get; set; }
        [Column(TypeName = "TEXT")]

        public string? OtpCode { get; set; }
        public DateTime? OtpExpiration { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
