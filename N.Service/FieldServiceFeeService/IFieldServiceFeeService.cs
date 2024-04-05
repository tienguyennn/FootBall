using N.Model.Entities;
using N.Service.FieldServiceFeeService.Dto;
using N.Service.Common;
using N.Service.Common.Service;
using N.Service.Dto;

namespace N.Service.FieldServiceFeeService
{
    public interface IFieldServiceFeeService : IService<FieldServiceFee>
    {
        Task<DataResponse<PagedList<FieldServiceFeeDto>>> GetData(FieldServiceFeeSearch search);
        Task<DataResponse<FieldServiceFeeDto>> GetDto(Guid id);
    }
}
