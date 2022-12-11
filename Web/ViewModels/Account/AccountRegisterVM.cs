using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.ViewModels.Account
{
    public class AccountRegisterVM
    {
        [Required, MaxLength(50)]
        public string Fullname { get; set; }
        [Required, MaxLength(30)]
        public string Username { get; set; }
        [Required, MaxLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MaxLength(30), DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }
        [Required, MaxLength(20), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MaxLength(20), DataType(DataType.Password), Display(Name = "Confirm Password"), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
