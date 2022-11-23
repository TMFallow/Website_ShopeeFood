using ShopeeFood_Data.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class Restaurant
    {
        public Restaurant()
        {
            Foods = new HashSet<Foods>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? RestaurantID { get; set; }

        [Required]
        public string NameofRestaurant { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime OpenTime { get; set; }

        public DateTime CloseTime { get; set; }

        public int? AreaID { get; set; }

        public int? ID { get; set; }

        public int? PromotionID { get; set; }

        public int? IDDetailsArea { get; set; }

        [ForeignKey("AreaID")]
        public virtual Areas Area { get; set; }

        [ForeignKey("ID")]
        public virtual Types Type { get; set; }

        [ForeignKey("PromotionID")]
        public virtual Promotion Promotion { get; set; }

        [ForeignKey("IDDetailsArea")]
        public virtual DetailAreas DetailAreas { get; set; }

        public ICollection<Foods> Foods { get; set; }
    }
}