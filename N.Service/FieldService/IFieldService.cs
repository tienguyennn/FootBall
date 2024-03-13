using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using N.Model.Entities;
using N.Service.Common;
using N.Service.Common.Service;
using N.Service.DTO;
using N.Service.FieldService.Dto;

namespace N.Service.FieladService
{
    public interface IFieldService : IService<Field>
    {
        DataResponse<PagedList<FieldDto>> GetData(FieldSearch search);
    }
}
