using Microsoft.EntityFrameworkCore;
using N.Model.Entities;


namespace N.Repository.ServiceFeePaymentRepository
{
    public class ServiceFeePaymentRepository : Repository<ServiceFeePayment>, IServiceFeePaymentRepository
    {
        public ServiceFeePaymentRepository(DbContext context) : base(context)
        {
        }
    }
}
