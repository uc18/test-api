using System.ComponentModel.DataAnnotations;

namespace MySolution.Web.DTO
{
    public class NewUser
    {
        [MaxLength(15)]
        public string Login { get; set; }

        [MaxLength(10)]
        public string Password { get; set; }
    }
}