using System.ComponentModel.DataAnnotations;

namespace N.Api.ViewModels.Account
{
    public class AppUserEditViewModel
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Type { get; set; }
        public List<Guid>? AreaIds { get; set; }
    }
}
