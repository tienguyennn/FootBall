using System.ComponentModel.DataAnnotations;

namespace N.Api.ViewModels.Account
{
    public class RegisterViewModel
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Type { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public List<Guid>? AreaIds { get; set; }
    }
}
