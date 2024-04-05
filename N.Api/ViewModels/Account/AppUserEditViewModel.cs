using System.ComponentModel.DataAnnotations;

namespace N.Api.ViewModels.Account
{
    public class AppUserEditViewModel
    {
        public string? FamilyName { get; set; }
        public string? Name { get; set; }
        public string? GivenName { get; set; }
        public string? Gender { get; set; }
    }
}
