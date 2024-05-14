using Microsoft.AspNetCore.Identity;

namespace N.Model.Entities
{
    public class Invite : Entity
    {
        public Guid? TeamId { get; set; }
        public Guid? InviteTeamId { get; set; }
        public bool? Accepted { get; set; }
        public DateTime? EnviteTime { get; set; }
        public string? Description { get; set; }
        public Guid? BookingId { get; set; }
    }
}
