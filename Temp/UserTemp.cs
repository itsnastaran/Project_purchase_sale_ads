using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temp
{
    public class UserTemp
    {
        public string Id { get; set; }
        [MaxLength(256)]
        [Display(Name ="نام و نام خانوادگی")]
        public string NameFamily { get; set; }
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [MaxLength(11)]
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }
        [MaxLength(256)]
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [MaxLength(10)]
        [Display(Name = "کدپستی")]
        public string Postalcode { get; set; }
        
        [Display(Name = "فروشنده؟")]
        public bool? SellerStatus { get; set; }
    }
}
