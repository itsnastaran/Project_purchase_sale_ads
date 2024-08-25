using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class Payment
    {
        [Key]
        public int ID { get; set; }
        public string Authority { get; set; }
        public string RefId { get; set; }
        public int AdId { get; set; }
        public string UserId { get; set; }
        public string Price { get; set; }
        public bool Status { get; set; }
    }
}
