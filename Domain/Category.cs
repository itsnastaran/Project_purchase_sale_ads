using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class Category
    {
        [Key]
        public int ID { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage =ErrorMessage.RequiredMsg)]
        [MaxLength(250, ErrorMessage =ErrorMessage.MaxLengthMsg)]
        [Display(Name ="عنوان")]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "تصویر")]
        public string Icon { get; set; }

        public virtual ICollection<SubCategory> _SubCategory_Categories { get; set; }
        public virtual ICollection<CategoryDetails> _CategoriesCategoryDetails { get; set; }
    }
}
