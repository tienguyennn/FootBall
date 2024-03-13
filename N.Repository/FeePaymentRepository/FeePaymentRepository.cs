using Microsoft.EntityFrameworkCore;
using N.Model.Entities;


namespace N.Repository.FeePaymentRepository
{
    public class FeePaymentRepository : Repository<FeePayment>, IFeePaymentRepository
    {
        public FeePaymentRepository(DbContext context) : base(context)
        {
        }
    }
}
