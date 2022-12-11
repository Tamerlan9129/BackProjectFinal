using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Account
{
    public class AccountLoginVM
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
