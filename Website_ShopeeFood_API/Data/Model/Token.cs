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
    public class Token
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TokenID { get; set; }

        public string? refreshToken { get; set; }   

        public DateTime refreshTokenExpireTime { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User user { get; set; }
    }
}
