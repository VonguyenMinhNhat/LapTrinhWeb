﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoNguyenMinhNhat_KTGK.Models
{
    [Table("NganhHoc")]
    public class NganhHoc
    {
        [Key]
        [StringLength(4)]
        public string MaNganh { get; set; }

        [Required]
        [StringLength(30)]
        public string TenNganh { get; set; }

        public virtual ICollection<SinhVien> SinhViens { get; set; }
    }
}
