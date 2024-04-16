using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using N.Api.ViewModels;
using N.Model.Entities;
using N.Service.Common;
using N.Service.Dto;
using N.Service.FieldAreaService.Dto;
using N.Service.ServiceFeeService;
using N.Service.ServiceFeeService.Dto;

namespace N.Controllers
{
    [Route("api/[controller]")]
    public class ServiceFeeController : NController
    {
        private readonly IServiceFeeService _ServiceFeeService;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceFeeController> _logger;

        public ServiceFeeController(
            IServiceFeeService ServiceFeeService,
            IMapper mapper,
            ILogger<ServiceFeeController> logger
            )
        {
            this._ServiceFeeService = ServiceFeeService;
            this._mapper = mapper;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<DataResponse<ServiceFee>> Create([FromBody] ServiceFeeCreateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = new ServiceFee()
                    {
                        Name = model.Name,
                        Icon = model.Icon,
                        Price = model.Price,
                        Description = model.Description,
                        UserId = UserId,
                    };
                    await _ServiceFeeService.Create(entity);
                    return new DataResponse<ServiceFee>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    DataResponse<ServiceFee>.False("Error", new string[] { ex.Message });
                }
            }
            return DataResponse<ServiceFee>.False("Some properties are not valid", ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)));
        }

        [HttpPost("Edit")]
        public async Task<DataResponse<ServiceFee>> Edit([FromBody] ServiceFeeEditVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _ServiceFeeService.GetById(model.Id);
                    if (entity != null)
                    {
                        entity.Name = model.Name;
                        entity.Icon = model.Icon;
                        entity.Price = model.Price;
                        entity.Description = model.Description;
                    }

                    await _ServiceFeeService.Update(entity);
                    return new DataResponse<ServiceFee>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    DataResponse<ServiceFee>.False("Error", new string[] { ex.Message });
                }
            }
            return DataResponse<ServiceFee>.False("Some properties are not valid", ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)));
        }

        //[HttpPost("GetData")]
        //public DataResponse<PagedList<ServiceFeeDto>> GetData([FromBody] FieldAreaSearch search)
        //{
        //    return _ServiceFeeService.GetData(search);
        //}

        [HttpGet("GetAll")]
        public async Task<DataResponse<List<ServiceFee>>> GetAll()
        {
            var data = await _ServiceFeeService.GetQueryable().Where(x=>x.UserId == UserId).ToListAsync();

            return new DataResponse<List<ServiceFee>>
            {
                Success = true,
                Message = "Success",
                Data = data,
            };
        }

        [HttpGet("Get/{id}")]
        public async Task<DataResponse<ServiceFee>> Get(Guid id)
        {
            try
            {
                var entity = _ServiceFeeService.GetById(id);
                return DataResponse<ServiceFee>.True(entity);
            }
            catch (Exception ex)
            {

                return DataResponse<ServiceFee>.False(ex.Message);
            }
        }


        [HttpPost("Delete/{id}")]
        public async Task<DataResponse> Delete(Guid id)
        {
            try
            {
                var entity = _ServiceFeeService.GetById(id);
                await _ServiceFeeService.Delete(entity);
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