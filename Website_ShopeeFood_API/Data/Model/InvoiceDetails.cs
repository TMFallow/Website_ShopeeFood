using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class InvoiceDetails
    {
        public int? InvoicesID { get; set; }

        public int? FoodId { get; set; }

        public string NameofFood { get; set; }

        public string Images { get; set; }

        public float Price { get; set; }

        public int Numbers { get; set; }

        //[ForeignKey("InvoicesID")]
        //public Invoices Invoices { get; set; }

        //[ForeignKey("FoodId")]
        //public Foods Foods { get; set; }
    }
}
