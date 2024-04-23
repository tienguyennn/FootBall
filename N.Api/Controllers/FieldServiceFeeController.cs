using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using N.Api.ViewModels;
using N.Model.Entities;
using N.Service.FieldServiceFeeService;
using N.Service.FieldServiceFeeService.Dto;
using N.Service.Common;
using N.Service.Dto;

namespace N.Controllers
{
    [Route("api/[controller]")]
    public class FieldServiceFeeController : NController
    {
        private readonly IFieldServiceFeeService _fieldServiceFeeService;
        private readonly IMapper _mapper;
        private readonly ILogger<FieldServiceFeeController> _logger;


        public FieldServiceFeeController(
            IFieldServiceFeeService fieldServiceFeeService,
            IMapper mapper,
            ILogger<FieldServiceFeeController> logger
            )
        {
            this._fieldServiceFeeService = fieldServiceFeeService;
            this._mapper = mapper;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<DataResponse<FieldServiceFee>> Create([FromBody] FieldServiceFeeCreateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = new FieldServiceFee()
                    {
                        Price = model.Price,
                        FieldId = model.FieldId,
                        ServiceFeeId = model.ServiceFeeId,
                    };

                    await _fieldServiceFeeService.Create(entity);
                    return new DataResponse<FieldServiceFee>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    return DataResponse<FieldServiceFee>.False("Error", new string[] { ex.Message });
                }
            }
            return DataResponse<FieldServiceFee>.False("Some properties are not valid", ModelStateError);
        }

        [HttpPost("Edit")]
        public async Task<DataResponse<FieldServiceFee>> Edit([FromBody] FieldServiceFeeEditVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _fieldServiceFeeService.GetById(model.Id);
                    if (entity == null)
                        return DataResponse<FieldServiceFee>.False("FieldServiceFee not found");
                    entity.Price = model.Price;
                    await _fieldServiceFeeService.Update(entity);
                    return new DataResponse<FieldServiceFee>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    DataResponse<FieldServiceFee>.False(ex.Message);
                }
            }
            return DataResponse<FieldServiceFee>.False("Some properties are not valid", ModelStateError);
        }
        [HttpGet("Get/{id}")]
        public async Task<DataResponse<FieldServiceFeeDto>> Get(Guid id)
        {
            return await _fieldServiceFeeService.GetDto(id);
        }

        [HttpPost("GetData")]
        public async Task<DataResponse<PagedList<FieldServiceFeeDto>>> GetData([FromBody] FieldServiceFeeSearch search)
        {
            return await _fieldServiceFeeService.GetData(search);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<DataResponse> Delete(Guid id)
        {
            try
            {
                var entity = _fieldServiceFeeService.GetById(id);
                await _fieldServiceFeeService.Delete(entity);
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


    }
}