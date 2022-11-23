using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AddressToDelivery
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? ID { get; set; }

        public int? UserID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumbers { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
