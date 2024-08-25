using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class SaleAd
    {
        [Key]
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(250, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [Display(Name = "مبلغ")]
        public long Price { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(450, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [Display(Name = "متن")]
        public string Text { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(20, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "تاریخ ثبت")]
        public string Date { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [Display(Name = "وضعیت")]
        public bool Status { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [Display(Name = "تعداد بازدید")]
        public int Visits { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [Display(Name = "آگهی ویژه؟")]
        public bool Special { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(20, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "تاریخ اتقضا")]
        public string SpecialDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(250, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "برند")]
        public string Brand { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(250, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "مدل")]
        public string Model { get; set; }
        [Display(Name = "کاربر")]
        public string UserID { get; set; }
        [Display(Name = "نوع آگهی")]
        public int TypeOfAdId { get; set; }
        public int ProductTypeId { get; set; }
        public int CategryID { get; set; }
        public int SubCategoryId { get; set; }

        [ForeignKey(nameof(TypeOfAdId))]
        [Display(Name = "نوع آگهی")]
        public virtual TypeOfAd _SaleAd_TypeOfAd { get; set; }

        [ForeignKey(nameof(ProductTypeId))]
        [Display(Name = "نوع کالا")]
        public virtual ProductType _SaleAd_ProductType { get; set; }

        [ForeignKey(nameof(CategryID))]
        public virtual Category _SaleAd_Categories { get; set; }

        [ForeignKey(nameof(SubCategoryId))]
        public virtual SubCategory _SaleAd_SubCategory { get; set; }

        public virtual ICollection<PhotoGallery> _SaleAd_PhotoGallery { get; set; }
        public virtual ICollection<Favorties> _SaleAd_Favorties { get; set; }
        public virtual ICollection<CategoryDetailsAd> _SaleAd_CategoryDetailsAd { get; set; }

    }
}
