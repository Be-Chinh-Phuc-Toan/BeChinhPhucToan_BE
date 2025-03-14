using System.Text.Json.Serialization;

namespace BeChinhPhucToan_BE.Models
{
    public class StudentReward
    {
        public int id { get; set; }
        public int StudentId { get; set; }
        public int quantity { get; set; }
        public Student? Student { get; set; }
        public int RewardId { get; set; }
        public Reward? Reward { get; set; }
        public DateTime? AchievedDate { get; set; }
    }
}
