using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoNguyenMinhNhat_KTGK.Models
{
    public class ChiTietDangKy
    {
        [Key, Column(Order = 0)]
        public int MaDK { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(6)]
        public string MaHP { get; set; }

        [ForeignKey("MaDK")]
        public virtual DangKy DangKy { get; set; }

        [ForeignKey("MaHP")]
        public virtual HocPhan HocPhan { get; set; }
    }
}
