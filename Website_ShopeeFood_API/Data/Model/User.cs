using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class User
    {

        public User()
        {
            AddressToDeliveries = new HashSet<AddressToDelivery>();
            invoices = new HashSet<Invoices>();
            Promotions = new HashSet<Promotion>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? UserId { get; set; }

        [Required]
        [Column(TypeName ="varchar(20)")]
        public string Username { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(12)")]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        public string Image { get; set; }

        [Required]
        public string Sex { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Column(TypeName ="varchar(11)")]
        public string PhoneNumber { get; set; }

        public string Token { get; set; }

        public ICollection<AddressToDelivery> AddressToDeliveries { get; set; }

        public ICollection<Invoices> invoices { get; set; }

        public ICollection<Promotion> Promotions { get; set; }
    }
}
