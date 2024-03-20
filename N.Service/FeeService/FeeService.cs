using N.Model.Entities;
using N.Repository.FeeRepository;
using N.Service.Common.Service;

namespace N.Service.FeeService
{
    public class FeeService : Service<Fee>, IFeeService
    {
        public FeeService(
            IFeeRepository FeeRepository
            ) : base(FeeRepository)
        {
        }


    }
}
