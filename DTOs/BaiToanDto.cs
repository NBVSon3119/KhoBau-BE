using System.ComponentModel.DataAnnotations;

namespace KhoBau_BE.DTOs
{
    public class BaiToanDto
    {
        [Range(1, 500)]
        public int SoHang { get; set; }
        [Range(1, 500)]
        public int SoCot { get; set; }
        [Range(1, int.MaxValue)]
        public int SoP { get; set; }

        [Required]
        public int[][] MaTran { get; set; } = Array.Empty<int[]>();
    }
}