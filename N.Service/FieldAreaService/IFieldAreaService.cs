using N.Model.Entities;
using N.Service.FieldAreaService.Dto;
using N.Service.Common;
using N.Service.Common.Service;
using N.Service.Dto;

namespace N.Service.FieldAreaService
{
    public interface IFieldAreaService : IService<FieldArea>
    {
        DataResponse<PagedList<FieldAreaDto>> GetData(FieldAreaSearch search);
        DataResponse<FieldAreaDto> GetDto(Guid id);
    }
}
