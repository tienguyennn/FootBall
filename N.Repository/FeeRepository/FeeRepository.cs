using Microsoft.EntityFrameworkCore;
using N.Model.Entities;


namespace N.Repository.FeeRepository
{
    public class FeeRepository : Repository<Fee>, IFeeRepository
    {
        public FeeRepository(DbContext context) : base(context)
        {
        }
    }
}
