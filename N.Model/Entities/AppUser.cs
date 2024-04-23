using Microsoft.AspNetCore.Identity;

namespace N.Model.Entities
{
    public class AppUser :  IdentityUser<Guid>
    {
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Picture { get; set; }
        public string? Type { get; set; }
        public Guid? StaffId { get; set; }
        public string? AreaIds { get; set; }
    }
}
