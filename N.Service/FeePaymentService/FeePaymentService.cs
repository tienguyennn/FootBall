using N.Model.Entities;
using N.Repository.ServiceFeePaymentRepository;
using N.Service.Common.Service;

namespace N.Service.ServiceFeePaymentService
{
    public class ServiceFeePaymentService : Service<ServiceFeePayment>, IServiceFeePaymentService
    {
        public ServiceFeePaymentService(
            IServiceFeePaymentRepository ServiceFeePaymentRepository
            ) : base(ServiceFeePaymentRepository)
        {
        }


    }
}
