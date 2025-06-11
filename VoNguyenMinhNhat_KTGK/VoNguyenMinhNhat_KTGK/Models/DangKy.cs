using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoNguyenMinhNhat_KTGK.Models
{
    public class DangKy
    {
        [Key]
        public int MaDK { get; set; }

        public DateTime NgayDK { get; set; }

        [StringLength(10)]
        public string MaSV { get; set; }

        [ForeignKey("MaSV")]
        public virtual SinhVien SinhVien { get; set; }

        public virtual ICollection<ChiTietDangKy> ChiTietDangKys { get; set; }
    }
}
