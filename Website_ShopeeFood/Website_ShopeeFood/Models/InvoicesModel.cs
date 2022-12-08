using System;

namespace Website_ShopeeFood.Models
{
    public class InvoicesModel
    {
        public int InvoicesID { get; set; }

        public int UserID { get; set; }

        public DateTime DeliveryDate { get; set; }

        public double TotalPrices { get; set; }

        public string Status { get; set; }

        public string Details { get; set; }
    }
}
