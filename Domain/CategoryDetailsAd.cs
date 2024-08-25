using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class CategoryDetailsAd
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "آگهی")]
        public int IDAd { get; set; }

        [Display(Name = "گروه ")]
        public int IDCategoryDetails { get; set; }

        [ForeignKey(nameof(IDCategoryDetails))]
        public virtual CategoryDetails _CategoryDetailsAd_CategoryDetails { get; set; }
        [ForeignKey(nameof(IDAd))]
        public virtual SaleAd _CategoryDetailsAd_SaleAd { get; set; }
    }
}
