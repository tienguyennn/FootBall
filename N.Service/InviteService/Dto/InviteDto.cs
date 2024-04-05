using N.Model.Entities;

namespace N.Service.InviteService.Dto
{
    public class InviteDto : Invite
    {
        public Team? Team { get; set; }
        public Team? InviteTeam { get; set; }
    }
}
