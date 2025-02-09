using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeChinhPhucToan_BE.Models
{
    [PrimaryKey(nameof(studentID), nameof(lessonTypeID))]
    public class Goal : BaseEntity
    {
        public int studentID { get; set; }
        public Student? Student { get; set; }
        public int lessonTypeID { get; set; }
        public LessonType? LessonType { get; set; }

        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public int numberLesson { get; set; }
        //public string typeLesson { get; set; }
        //public int badgeID { get; set; }
        public int reward { get; set; }
    }
}
