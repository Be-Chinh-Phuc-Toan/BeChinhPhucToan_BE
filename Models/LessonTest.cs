using System.ComponentModel.DataAnnotations.Schema;

namespace BeChinhPhucToan_BE.Models
{
    public class LessonTest
    {
        public int id { get; set; }
        public int code { get; set; }
        [Column(TypeName = "TEXT")]
        public string question { get; set; }
        [Column(TypeName = "TEXT")]
        public string answer { get; set; }

        public int lessonTypeID { get; set; }
        public LessonType? LessonType { get; set; }
    }
}
