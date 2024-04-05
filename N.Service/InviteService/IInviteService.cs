using N.Model.Entities;
using N.Service.InviteService.Dto;
using N.Service.Common;
using N.Service.Common.Service;
using N.Service.Dto;

namespace N.Service.InviteService
{
    public interface IInviteService : IService<Invite>
    {
        DataResponse<PagedList<InviteDto>> GetData(InviteSearch search);
        DataResponse<InviteDto> GetDto(Guid id);
    }
}
