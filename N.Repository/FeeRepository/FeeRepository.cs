using Microsoft.EntityFrameworkCore;
using N.Model.Entities;


namespace N.Repository.ServiceFeeRepository
{
    public class ServiceFeeRepository : Repository<ServiceFee>, IServiceFeeRepository
    {
        public ServiceFeeRepository(DbContext context) : base(context)
        {
        }
    }
}
