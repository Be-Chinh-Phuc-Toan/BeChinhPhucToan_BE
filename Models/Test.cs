using System.ComponentModel.DataAnnotations.Schema;

namespace BeChinhPhucToan_BE.Models
{
    public class Test : BaseEntity
    {
        public int id { get; set; }
        public int grade { get; set; }
        public int code { get; set; }
        [Column(TypeName = "TEXT")]
        public string question { get; set; }
        [Column(TypeName = "TEXT")]
        public string answer { get; set; }
    }
}
