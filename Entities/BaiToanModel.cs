namespace KhoBau_BE.Entities
{
    public class BaiToanModel
    {
        public int Id { get; set; }
        public int SoHang { get; set; }   // n
        public int SoCot { get; set; }    // m
        public int SoP { get; set; }      // p
        public string MaTranJson { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;   
    }
}