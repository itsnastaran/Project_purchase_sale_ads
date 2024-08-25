using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class Report
    {
        [Key]
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(250, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "فعال؟")]
        public bool status { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequiredMsg)]
        [MaxLength(20, ErrorMessage = ErrorMessage.MaxLengthMsg)]
        [Display(Name = "تاریخ ثبت")]
        public string Date { get; set; }
        [Display(Name = "دسته اصلی")]
        public int AdID { get; set; }


        [ForeignKey(nameof(AdID))]
        [Display(Name = "شناسه آگهی")]
        public virtual SaleAd _Report_SaleAd { get; set; }
    }
}
