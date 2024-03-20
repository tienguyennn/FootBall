using N.Model.Entities;
using N.Repository.FeePaymentRepository;
using N.Service.Common.Service;

namespace N.Service.FeePaymentService
{
    public class FeePaymentService : Service<FeePayment>, IFeePaymentService
    {
        public FeePaymentService(
            IFeePaymentRepository FeePaymentRepository
            ) : base(FeePaymentRepository)
        {
        }


    }
}
