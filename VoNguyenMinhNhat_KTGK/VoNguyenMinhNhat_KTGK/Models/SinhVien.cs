using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoNguyenMinhNhat_KTGK.Models
{
    [Table("SinhVien")]
    public class SinhVien
    {
        [Key]
        [StringLength(10)]
        public string MaSV { get; set; }

        [Required]
        [StringLength(50)]
        public string HoTen { get; set; }

        [StringLength(5)]
        public string? GioiTinh { get; set; }

        [DataType(DataType.Date)]
        public DateTime? NgaySinh { get; set; }

        [StringLength(100)]
        public string? Hinh { get; set; }  // ✅ Cho phép null

        [Required]
        [StringLength(4)]
        public string MaNganh { get; set; }

        [ForeignKey("MaNganh")]
        public virtual NganhHoc? NganhHoc { get; set; }
    }
}
