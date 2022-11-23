using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Invoices
    {
        public Invoices()
        {
            invoiceDetails = new HashSet<InvoiceDetails>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? InvoicesID { get; set; }

        public int? UserID { get; set; }

        public DateTime DeliveryDate { get; set; }

        [NotMapped]
        public float TotalPrices => invoiceDetails.Sum(s => s.Total);

        public string Status { get; set; }

        public string Details { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        public ICollection<InvoiceDetails> invoiceDetails { get; set; }
    }
}
