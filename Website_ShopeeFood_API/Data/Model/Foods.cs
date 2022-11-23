using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Foods
    {
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int FoodId { get; set; }

        [Required]
        public string NameofFood { get; set; }

        [Required]
        public string Images { get; set; }


        public string Description { get; set; }

        [Required]
        [Column(TypeName ="float")]
        public float Price { get; set; }

        public string TypeofFood { get; set; }

        public int Quantity { get; set; }

        public int? RestaurantID { get; set; }

        [ForeignKey("RestaurantID")]
        public Restaurant Restaurant { get; set; }
    }
}
