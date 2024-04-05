using N.Model.Entities;
using N.Repository.ServiceFeeRepository;
using N.Service.Common.Service;

namespace N.Service.ServiceFeeService
{
    public class ServiceFeeService : Service<ServiceFee>, IServiceFeeService
    {
        public ServiceFeeService(
            IServiceFeeRepository ServiceFeeRepository
            ) : base(ServiceFeeRepository)
        {
        }


    }
}
