using System.ComponentModel.DataAnnotations;

namespace N.Api.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
