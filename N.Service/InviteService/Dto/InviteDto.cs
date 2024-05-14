using N.Model.Entities;
using N.Service.BookingService.Dto;

namespace N.Service.InviteService.Dto
{
    public class InviteDto : Invite
    {
        public Team? Team { get; set; }
        public Team? InviteTeam { get; set; }
        public BookingDto? Booking { get; set; }
    }
}
