using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Types
    {
        public Types()
        {
            restaurants = new HashSet<Restaurant>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(30)")]
        public string NameofType { get; set; }

        public ICollection<Restaurant> restaurants { get; set; }
    }
}
