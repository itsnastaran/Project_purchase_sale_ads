using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class AboutUs
    {
        [Key]
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(250, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(450, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(250, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(250, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "کلمات کلیدی")]
        public string KeyWords { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(11, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "تلفن")]
        public string Tel { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(11, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(250, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "آدرس")]
        public string Address { get; set; }
    }
}
