using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Projectcore.Data
{
    public class ApplicationUser:IdentityUser
    {
        [MaxLength(256)]
        [Display(Name ="نام و نام خانوادگی")]
        public string? NameFamily { get; set; }
        [Display(Name ="آدرس")]
        [MaxLength(256)]
        public string? Address { get; set; }
        [Display(Name ="کدپستی")]
        [MaxLength(10)]
        public string? Postalcode { get; set; }
        [Display(Name ="فروشنده؟")]
        public bool? SellerStatus { get; set; }
    }
}
