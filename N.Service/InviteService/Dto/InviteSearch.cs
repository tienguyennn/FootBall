using N.Service.Common;

namespace N.Service.InviteService.Dto
{
    public class InviteSearch : SearchBase
    {
        public Guid? UserId { get; set; }
        public Guid? UserInviteId { get; set; }
        public bool? Accept { get; set; }
        public bool? All { get; set; }
    }
}
