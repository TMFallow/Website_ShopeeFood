using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_Data.Model
{
    public class DetailAreas
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? IDDetailsArea { get; set; }

        [Required]
        public string NameofDetailsArea { get; set; }

        public int? AreaID { get; set; }

        [ForeignKey("AreaID")]
        public Areas Areas { get; set; }
    }
}
