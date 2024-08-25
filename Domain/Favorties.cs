using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class Favorties
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "شناسه کاربر")]
        public string UserID { get; set; }
        [Display(Name = "شناسه آگهی")]
        public int SaleAdId { get; set; }
        [ForeignKey(nameof(SaleAdId))]
        [Display(Name = "نام آگهی")]
        public virtual SaleAd _Favorties_SaleAdId { get; set; } 
    }
}
