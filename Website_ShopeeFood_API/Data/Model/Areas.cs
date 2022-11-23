using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Areas
    {
        public Areas()
        {
            Restaurants = new List<Restaurant>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? AreaID { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        public string NameofArea { get; set; }

        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
