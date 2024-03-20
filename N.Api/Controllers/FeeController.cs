using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using N.Api.ViewModels;
using N.Model.Entities;
using N.Service.DTO;
using N.Service.FeeService;

namespace N.Controllers
{
    [Route("api/[controller]")]
    public class FeeController : NController
    {
        private readonly IFeeService _FeeService;
        private readonly IMapper _mapper;
        private readonly ILogger<FeeController> _logger;

        public FeeController(
            IFeeService FeeService,
            IMapper mapper,
            ILogger<FeeController> logger
            )
        {
            this._FeeService = FeeService;
            this._mapper = mapper;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<DataResponse<Fee>> Create([FromBody] FeeCreateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = new Fee()
                    {
                        FieldId = model.FieldId,
                        Price = model.Price,
                        Name = model.Name,
                        Description = model.Description,
                    };
                    await _FeeService.Create(entity);
                    return new DataResponse<Fee>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    DataResponse<Fee>.False("Error", new string[] { ex.Message });
                }
            }
            return DataResponse<Fee>.False("Some properties are not valid", ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)));
        }


    }
}