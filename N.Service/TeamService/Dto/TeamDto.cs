using N.Model.Entities;

namespace N.Service.TeamService.Dto
{
    public class TeamDto : Team
    {
        public Field? Field { get; set; }
    }
}
