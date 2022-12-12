using System.Collections.Generic;

namespace Website_ShopeeFood.Models
{
    public class InvoicesDetailByIDModel
    {
        public InvoicesModel invoicesModel { get; set; }

        public List<InvoiceDetailsModel> invoiceDetails { get; set; }
    }
}
