using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Website_ShopeeFood.Models
{
    public class FoodModel
    {
        public int FoodId { get; set; }

        public string NameofFood { get; set; }

        public string Images { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string TypeofFood { get; set; }

        public int Quantity { get; set; }

        public int RestaurantID { get; set; }
    }
}
