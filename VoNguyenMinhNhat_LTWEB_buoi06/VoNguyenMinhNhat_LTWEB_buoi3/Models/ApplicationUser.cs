using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VoNguyenMinhNhat_LTWEB_buoi3.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Age { get; set; }
    }
}
