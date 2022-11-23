using System.ComponentModel.DataAnnotations;

namespace Website_ShopeeFood.Models
{
    public class PromotionModels
    {
        public int? PromotionID { get; set; }

        public string PromotionName { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }
    }
}
