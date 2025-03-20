using System.ComponentModel.DataAnnotations.Schema;

namespace BeChinhPhucToan_BE.Models
{
    public class Question : BaseEntity
    {
        public int id { get; set; }
        [Column(TypeName = "TEXT")]
        public string question { get; set; }
        [Column(TypeName = "TEXT")]
        public string answer { get; set; }
        [Column(TypeName = "TEXT")]
        public string wrongAnswer1 { get; set; }
        [Column(TypeName = "TEXT")]
        public string wrongAnswer2 { get; set; }
        [Column(TypeName = "TEXT")]
        public string wrongAnswer3 { get; set; }
        [Column(TypeName = "TEXT")]
        public string solution { get; set; }


        public int exerciseID { get; set; }
        public Exercise? Exercise { get; set; }
    }
}
