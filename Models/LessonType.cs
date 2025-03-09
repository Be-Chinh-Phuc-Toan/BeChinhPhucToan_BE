namespace BeChinhPhucToan_BE.Models
{
    public class LessonType : BaseEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public int grade { get; set; }
        public int operationId { get; set; }

        // Navigation property (nếu dùng Entity Framework)
        public Operations Operation { get; set; }
    }
}
