using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temp
{
    public class ContactUsTemp
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(250, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(450, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "توضیحات")]
        public string Message { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(250, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "نام و نام خانوادگی")]

        public string NameFamily { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(250, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "ایمیل")]
        
        [DataType(DataType.EmailAddress, ErrorMessage = "فرمت ایمیل صحیح نمی باشد")]
        public string Email { get; set; }
    }
}
