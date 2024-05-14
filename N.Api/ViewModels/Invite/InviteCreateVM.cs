using Microsoft.AspNetCore.Identity;

namespace N.Api.ViewModels
{
    public class InviteCreateVM
    {
        public Guid? BookingId { get; set; }
        public Guid? TeamId { get; set; }
        public Guid? InviteTeamId { get; set; }
        public string? Description { get; set; }
    }
}