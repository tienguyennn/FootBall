using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using N.Api.ViewModels;
using N.Model.Entities;
using N.Service.InviteService;
using N.Service.InviteService.Dto;
using N.Service.Common;
using N.Service.Dto;

namespace N.Controllers
{
    [Route("api/[controller]")]
    public class InviteController : NController
    {
        private readonly IInviteService _inviteService;
        private readonly IMapper _mapper;
        private readonly ILogger<InviteController> _logger;


        public InviteController(
            IInviteService inviteService,
            IMapper mapper,
            ILogger<InviteController> logger
            )
        {
            this._inviteService = inviteService;
            this._mapper = mapper;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<DataResponse<Invite>> Create([FromBody] InviteCreateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = new Invite()
                    {
                        TeamId = model.TeamId,
                        InviteTeamId = model.InviteTeamId,
                        Description = model.Description,
                        EnviteTime = DateTime.Now,
                        BookingId = model.BookingId,
                    };

                    await _inviteService.Create(entity);
                    return new DataResponse<Invite>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    return DataResponse<Invite>.False("Error", new string[] { ex.Message });
                }
            }
            return DataResponse<Invite>.False("Some properties are not valid", ModelStateError);
        }

        [HttpPost("Edit")]
        public async Task<DataResponse<Invite>> Edit([FromBody] InviteEditVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _inviteService.GetById(model.Id);
                    if (entity == null)
                        return DataResponse<Invite>.False("Invite not found");

                    await _inviteService.Update(entity);
                    return new DataResponse<Invite>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    DataResponse<Invite>.False(ex.Message);
                }
            }
            return DataResponse<Invite>.False("Some properties are not valid", ModelStateError);
        }
        [HttpGet("Get/{id}")]
        public async Task<DataResponse<InviteDto>> Get(Guid id)
        {
            return await _inviteService.GetDto(id);
        }

        [HttpPost("GetData")]
        public async Task<DataResponse<PagedList<InviteDto>>> GetData([FromBody] InviteSearch search)
        {
            return await _inviteService.GetData(search);
        }

        [HttpPost("Accept")]
        public async Task<DataResponse<Invite>> Accept([FromBody] AcceptVM model)
        {
            var entity = _inviteService.GetById(model.Id);
            try
            {
                if (entity != null)
                {
                    entity.Accepted = model.Accept;
                    await _inviteService.Update(entity);

                    var all = _inviteService.GetQueryable().Where(x => x.InviteTeamId == entity.InviteTeamId && x.Id != entity.Id);
                    foreach (var item in all)
                    {
                        item.Accepted = false;
                    }
                    await _inviteService.Update(all);
                }
                return new DataResponse<Invite>()
                {
                    Data = entity,
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return DataResponse<Invite>.False(ex.Message);
            }

        }

        [HttpGet("InviteMe")]
        public async Task<DataResponse<PagedList<InviteDto>>> InviteMe()
        {
            return await _inviteService.GetData(new InviteSearch()
            {
                UserId = UserId,
                PageSize = 10000,
                Accept = null,
                All = null,
            });
        }

        [HttpGet("IAccepted")]
        public async Task<DataResponse<PagedList<InviteDto>>> IAccepted()
        {
            return await _inviteService.GetData(new InviteSearch()
            {
                UserId = UserId,
                PageSize = 10000,
                Accept = true,
                All = null,
            });
        }

        [HttpGet("IReject")]
        public async Task<DataResponse<PagedList<InviteDto>>> IReject()
        {
            return await _inviteService.GetData(new InviteSearch()
            {
                UserId = UserId,
                PageSize = 10000,
                Accept = false,
                All = null,
            });
        }


        [HttpGet("IInvite")]
        public async Task<DataResponse<PagedList<InviteDto>>> IInvite()
        {
            return await _inviteService.GetData(new InviteSearch()
            {
                UserInviteId = UserId,
                PageSize = 10000,
                Accept = null,
                All = null,
            });
        }


        [HttpGet("AcceptedMe")]
        public async Task<DataResponse<PagedList<InviteDto>>> AcceptedMe()
        {
            return await _inviteService.GetData(new InviteSearch()
            {
                UserInviteId = UserId,
                PageSize = 10000,
                Accept = true,
                All = null,
            });
        }

        [HttpGet("RejectMe")]
        public async Task<DataResponse<PagedList<InviteDto>>> RejectMe()
        {
            return await _inviteService.GetData(new InviteSearch()
            {
                UserInviteId = UserId,
                PageSize = 10000,
                Accept = true,
                All = null,
            });
        }

    }
}