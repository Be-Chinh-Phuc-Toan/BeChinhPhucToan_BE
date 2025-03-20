using System.ComponentModel.DataAnnotations.Schema;

namespace BeChinhPhucToan_BE.Models
{
    public class GoalStatus : BaseEntity
    {
        public int id { get; set; }
        [Column(TypeName = "TEXT")]
        public string name { get; set; }
    }
}
