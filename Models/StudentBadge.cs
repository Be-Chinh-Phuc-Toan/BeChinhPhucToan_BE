namespace BeChinhPhucToan_BE.Models
{
    public class StudentBadge
    {
        public int id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int BadgeId { get; set; }
        public Badge Badge { get; set; }
        public DateTime? AchievedDate { get; set; }
    }
}
