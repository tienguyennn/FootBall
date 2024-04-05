using N.Model.Entities;
using N.Service.TeamService.Dto;
using N.Service.Common;
using N.Service.Common.Service;
using N.Service.Dto;

namespace N.Service.TeamService
{
    public interface ITeamService : IService<Team>
    {
        DataResponse<PagedList<TeamDto>> GetData(TeamSearch search, Guid ? userId = null);
        DataResponse<TeamDto> GetDto(Guid? id);
    }
}
