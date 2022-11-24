using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Website_ShopeeFood.Models
{
    public class UsersModel
    {
        public int? UserId { get; set; }

        [Required, Display(Name = "Username không được để trống")]
        public string Username { get; set; }

        [Required, Display(Name = "Password không được để trống")]
        public string Password { get; set; }

        public string FullName { get; set; }

        public string Image { get; set; }

        public string Sex { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Token { get; set; }
    }
}
