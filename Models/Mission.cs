namespace BeChinhPhucToan_BE.Models
{
    public class Mission
    {
        public int Id { get; set; } // Khóa chính
        public int ExerciseId { get; set; } // Khóa ngoại tham chiếu bài tập
        public int RewardId { get; set; } // Khóa ngoại tham chiếu phần thưởng
        public DateTime DateStart { get; set; } // Ngày bắt đầu
        public DateTime DateEnd { get; set; } // Ngày kết thúc

        // Liên kết với bảng Exercise
        public Exercise Exercise { get; set; }

        // Liên kết với bảng Reward
        public Reward Reward { get; set; }
    }
}
