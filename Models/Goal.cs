using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeChinhPhucToan_BE.Models
{
    public class Goal : BaseEntity
    {
        public int id { get; set; }

        public int studentID { get; set; }
        public Student? Student { get; set; }
        public int lessonTypeID { get; set; }
        public LessonType? LessonType { get; set; }

        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public int numberLesson { get; set; }
        public int badgeID { get; set; }
        public int reward { get; set; }
        public int statusID { get; set; }
        public GoalStatus? GoalStatus { get; set; }
    }
}
