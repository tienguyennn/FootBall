using Microsoft.EntityFrameworkCore;
using N.Model.Entities;


namespace N.Repository.TeamRepository
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(DbContext context) : base(context)
        {
        }
    }
}
