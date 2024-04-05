using Microsoft.EntityFrameworkCore;
using N.Model.Entities;


namespace N.Repository.InviteRepository
{
    public class InviteRepository : Repository<Invite>, IInviteRepository
    {
        public InviteRepository(DbContext context) : base(context)
        {
        }
    }
}
