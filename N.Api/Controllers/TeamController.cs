using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using N.Api.ViewModels;
using N.Model.Entities;
using N.Service.TeamService;
using N.Service.TeamService.Dto;
using N.Service.Common;
using N.Service.Dto;
using N.Service.BookingService;
using N.Service.InviteService.Dto;
using BitMiracle.LibTiff.Classic;

namespace N.Controllers
{
    [Route("api/[controller]")]
    public class TeamController : NController
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;
        private readonly IBookingService _bookingService;
        private readonly ILogger<TeamController> _logger;


        public TeamController(
            ITeamService teamService,
            IMapper mapper,
            IBookingService bookingService,
            ILogger<TeamController> logger
            )
        {
            this._teamService = teamService;
            this._mapper = mapper;
            this._bookingService = bookingService;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<DataResponse<Team>> Create([FromBody] TeamCreateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    //var team = _teamService.GetQueryable().FirstOrDefault(x => x.UserId == UserId);
                    //if (team != null)
                    //    return DataResponse<Team>.False("Users have a team " + team.Name);

                    var entity = new Team()
                    {
                        Description = model.Description,
                        Name = model.Name,
                        Age = model.Age,
                        UserId = UserId,
                        Status = model.Status,
                        Phone = model.Phone,
                        FieldId = model.FieldId,
                        Level = model.Level,
                    };

                    await _teamService.Create(entity);
                    return new DataResponse<Team>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    return DataResponse<Team>.False("Error", new string[] { ex.Message });
                }
            }
            return DataResponse<Team>.False("Some properties are not valid", ModelStateError);
        }


        [HttpPost("Edit")]
        public async Task<DataResponse<Team>> Edit([FromBody] TeamEditVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _teamService.GetById(model.Id);
                    if (entity == null)
                        return DataResponse<Team>.False("Team not found");

                    entity.Level = model.Level;
                    entity.Name = model.Name;
                    entity.Age = model.Age;
                    entity.Status = model.Status;
                    entity.Description = model.Description;
                    entity.Phone = model.Phone;
                    entity.FieldId = model.FieldId;
                    await _teamService.Update(entity);
                    return new DataResponse<Team>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    DataResponse<Team>.False(ex.Message);
                }
            }
            return DataResponse<Team>.False("Some properties are not valid", ModelStateError);
        }
        [HttpPost("Delete/{id}")]
        public async Task<DataResponse> Delete(Guid id)
        {
            try
            {
                var entity = _teamService.GetById(id);
                await _teamService.Delete(entity);
                return new DataResponse()
                {
                    Success = true,
                    Message = "Success",
                };
            }
            catch (Exception ex)
            {

                return DataResponse.False(ex.Message);
            }
        }

        [HttpGet("Get/{id}")]
        public DataResponse<TeamDto> Get(Guid id)
        {
            return _teamService.GetDto(id);
        }

        [HttpGet("GetByUser")]
        public DataResponse<List<Team>> GetByUser()
        {
            var teams = _teamService.GetQueryable().Where(x => x.UserId == UserId).ToList();
            return DataResponse<List<Team>>.True(teams);
        }

        [HttpPost("FindTeam")]
        public DataResponse<PagedList<TeamDto>> FindTeam([FromBody] TeamSearch search)
        {
            return _teamService.GetData(search, UserId);
        }

    }
}