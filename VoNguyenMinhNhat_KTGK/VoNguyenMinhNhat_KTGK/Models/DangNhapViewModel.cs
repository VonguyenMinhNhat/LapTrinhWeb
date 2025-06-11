using System;
using System.ComponentModel.DataAnnotations;

namespace VoNguyenMinhNhat_KTGK.Models
{
    public class DangNhapViewModel
    {
        [Required]
        public string MaSV { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }
    }
}
