using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class SubCategory
    {
        [Key]
        public int ID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(250, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name ="دسته اصلی")]
        public int CategoryID { get; set; }
        [ForeignKey(nameof(CategoryID))]
        [Display(Name = "دسته اصلی")]
        public virtual Category _SubCategory_Categories { get; set; }
    }
}
