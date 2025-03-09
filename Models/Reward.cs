namespace BeChinhPhucToan_BE.Models
{
    public class Reward : BaseEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public double point { get; set; }
        public bool isAvailable { get; set; }

    }
}
