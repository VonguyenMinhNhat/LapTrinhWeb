﻿using System.ComponentModel.DataAnnotations;

namespace VoNguyenMinhNhat_LTWEB_buoi3.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
        