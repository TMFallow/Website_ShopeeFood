namespace Website_ShopeeFood.Models
{
    public class InvoiceDetailsModel
    {
        public int? InvoicesID { get; set; }

        public int? FoodId { get; set; }

        public string NameofFood { get; set; }

        public string Images { get; set; }

        public float Price { get; set; }

        public int Numbers { get; set; }
    }
}
