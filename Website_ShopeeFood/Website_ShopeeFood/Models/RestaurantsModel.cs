using System;

namespace Website_ShopeeFood.Models
{
    public class RestaurantsModel
    {
        public int? RestaurantID { get; set; }

        public string NameofRestaurant { get; set; }

        public string Image { get; set; }

        public string Address { get; set; }

        public DateTime OpenTime { get; set; }

        public DateTime CloseTime { get; set; }

        public int? AreaID { get; set; }

        public int? ID { get; set; }

        public int? IDDetailsArea { get; set; }
    }
}
