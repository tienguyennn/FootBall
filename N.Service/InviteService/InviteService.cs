using N.Model.Entities;
using N.Repository.InviteRepository;
using N.Service.Common.Service;
using N.Service.InviteService.Dto;
using N.Service.Common;
using N.Service.Dto;
using N.Repository.NDirectoryRepository;
using N.Repository.TeamRepository;

namespace N.Service.InviteService
{
    public class InviteService : Service<Invite>, IInviteService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;

        public InviteService(
            IUserRepository userRepository,
            ITeamRepository teamRepository,
            IInviteRepository inviteRepository
            ) : base(inviteRepository)
        {
            this._userRepository = userRepository;
            this._teamRepository = teamRepository;
        }

        public DataResponse<PagedList<InviteDto>> GetData(InviteSearch search)
        {
            try
            {
                var query = from q in GetQueryable()
                            join team in _teamRepository.GetQueryable()
                            on q.TeamId equals team.Id
                            join inviteTeam in _teamRepository.GetQueryable()
                            on q.InviteTeamId equals inviteTeam.Id
                            select new InviteDto()
                            {
                                Id = q.Id,
                                Accepted = q.Accepted,
                                Description = q.Description,
                                EnviteTime = q.EnviteTime,
                                InviteTeamId = q.InviteTeamId,
                                TeamId = q.TeamId,
                                Team = team,
                                InviteTeam = inviteTeam,
                            };


                if (search.All == true)
                {

                }
                else
                {
                    query = query.Where(x => x.Accepted == search.Accept);
                }
                if (search.UserId.HasValue)
                {
                    query = query.Where(x => x.InviteTeam != null && x.InviteTeam.UserId == search.UserId);

                }
                if (search.UserInviteId.HasValue)
                {
                    query = query.Where(x => x.Team != null && x.Team.UserId == search.UserId);

                }

                var result = PagedList<InviteDto>.Create(query, search);
                return new DataResponse<PagedList<InviteDto>>()
                {
                    Data = result,
                    Message = "Success"
                };

            }
            catch (Exception ex)
            {
                return DataResponse<PagedList<InviteDto>>.False(ex.Message);
            }

        }

        public DataResponse<InviteDto> GetDto(Guid id)
        {
            try
            {
                var item = (from q in GetQueryable()
                            join team in _teamRepository.GetQueryable()
                            on q.TeamId equals team.Id
                            join inviteTeam in _teamRepository.GetQueryable()
                            on q.InviteTeamId equals inviteTeam.Id
                            select new InviteDto()
                            {
                                Id = q.Id,
                                Accepted = q.Accepted,
                                Description = q.Description,
                                EnviteTime = q.EnviteTime,
                                InviteTeamId = q.InviteTeamId,
                                TeamId = q.TeamId,
                                Team = team,
                                InviteTeam = inviteTeam,
                            }).FirstOrDefault();

                return new DataResponse<InviteDto>()
                {
                    Success = true,
                    Data = item,
                };

            }
            catch (Exception ex)
            {
                return DataResponse<InviteDto>.False(ex.Message);
            }
        }

    }
}
