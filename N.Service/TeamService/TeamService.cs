using N.Model.Entities;
using N.Repository.TeamRepository;
using N.Service.Common.Service;
using N.Service.TeamService.Dto;
using N.Service.Common;
using N.Service.Dto;
using N.Repository.BookingRepository;
using N.Repository.NDirectoryRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace N.Service.TeamService
{
    public class TeamService : Service<Team>, ITeamService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IUserRepository _userRepository;

        public TeamService(
            IBookingRepository bookingRepository,
            IFieldRepository fieldRepository,
            IUserRepository userRepository,
            ITeamRepository teamRepository
            ) : base(teamRepository)
        {
            this._bookingRepository = bookingRepository;
            this._fieldRepository = fieldRepository;
            this._userRepository = userRepository;
        }

        public DataResponse<PagedList<TeamDto>> GetData(TeamSearch search, Guid? userId = null)
        {
            try
            {
                var query = from q in GetQueryable().Where(x => x.Status == 1)
                            select new TeamDto()
                            {
                                Id = q.Id,
                                UserId = q.UserId,
                                Description = q.Description,
                                Name = q.Name,
                                Phone = q.Phone,
                                Age = q.Age,
                                Level = q.Level,
                                FieldId = q.FieldId,
                                CreatedDate = q.CreatedDate,
                            };

                if (userId.HasValue)
                {
                    query = query.Where(x => x.UserId != userId);
                }

                if (search != null)
                {

                }
                query = query.OrderByDescending(x => x.CreatedDate);
                var result = PagedList<TeamDto>.Create(query, search);
                foreach (var item in result.Items)
                {
                    if (item.FieldId.HasValue)
                    {
                        item.Field = _fieldRepository.GetById(item.FieldId.Value);
                    }

                }
                return new DataResponse<PagedList<TeamDto>>()
                {
                    Data = result,
                    Message = "Success"
                };

            }
            catch (Exception ex)
            {
                return DataResponse<PagedList<TeamDto>>.False(ex.Message);
            }

        }

        public DataResponse<TeamDto> GetDto(Guid? id)
        {
            var query = from q in GetQueryable()
                        .Where(x => x.Id == id)
                        select new TeamDto()
                        {
                            Id = q.Id,
                            UserId = q.UserId,
                            Description = q.Description,
                            Name = q.Name,
                            Phone = q.Phone,
                            Age = q.Age,
                            FieldId = q.FieldId,
                            Level = q.Level,
                        };
            var data = query.FirstOrDefault();
            if (data != null)
            {
                if (data.FieldId.HasValue)
                {
                    data.Field = _fieldRepository.GetById(data.FieldId.Value);
                }
            }
            return new DataResponse<TeamDto>()
            {
                Success = true,
                Data = data
            };
        }
    }
}
