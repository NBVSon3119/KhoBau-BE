namespace KhoBau_BE.Entities
{
    public class KetQuaModel
    {
        public int Id { get; set; }
        public int BaiToanId { get; set; }
        public double NhienLieu { get; set; }
        public string PathJson { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}