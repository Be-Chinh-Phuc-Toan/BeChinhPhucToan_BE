namespace BeChinhPhucToan_BE.Models
{
    public class StudentMission
    {
        public int id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int MissionId { get; set; }
        public Mission Mission { get; set; }
        public DateTime? AchievedDate { get; set; }
    }
}
