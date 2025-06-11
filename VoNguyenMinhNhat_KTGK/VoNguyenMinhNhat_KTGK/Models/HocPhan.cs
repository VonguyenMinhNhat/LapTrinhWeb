using System.ComponentModel.DataAnnotations;

namespace VoNguyenMinhNhat_KTGK.Models
{
    public class HocPhan
    {
        [Key]
        [StringLength(6)]
        public string MaHP { get; set; }

        [Required]
        [StringLength(30)]
        public string TenHP { get; set; }

        public int SoTinChi { get; set; }
    }
}
