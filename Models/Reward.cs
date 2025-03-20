using System.ComponentModel.DataAnnotations.Schema;

namespace BeChinhPhucToan_BE.Models
{
    public class Reward : BaseEntity
    {
        public int id { get; set; }
        [Column(TypeName = "TEXT")]
        public string name { get; set; }
        [Column(TypeName = "TEXT")]
        public string image { get; set; }
        [Column(TypeName = "TEXT")]
        public string description { get; set; }
        public double point { get; set; }
        public bool isAvailable { get; set; }

    }
}
