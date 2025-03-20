using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeChinhPhucToan_BE.Models
{
    public class Exercise : BaseEntity
    {
        public int id { get; set; }
        [Column(TypeName = "TEXT")]
        public string name { get; set; }
        public bool isComplete { get; set; }

        public int lessonTypeID { get; set; }
        public LessonType? LessonType { get; set; }

    }
}
