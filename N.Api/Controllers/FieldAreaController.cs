using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using N.Api.ViewModels;
using N.Model.Entities;
using N.Service.FieldAreaService;
using N.Service.FieldAreaService.Dto;
using N.Service.Common;
using N.Service.Dto;
using Microsoft.EntityFrameworkCore;
using N.Service.FieldService.Dto;

namespace N.Controllers
{
    [Route("api/[controller]")]
    public class FieldAreaController : NController
    {
        private readonly IFieldAreaService _fieldAreaService;
        private readonly IMapper _mapper;
        private readonly ILogger<FieldAreaController> _logger;


        public FieldAreaController(
            IFieldAreaService fieldAreaService,
            IMapper mapper,
            ILogger<FieldAreaController> logger
            )
        {
            this._fieldAreaService = fieldAreaService;
            this._mapper = mapper;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<DataResponse<FieldArea>> Create([FromBody] FieldAreaCreateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = new FieldArea()
                    {
                        Name = model.Name,
                        Description = model.Description,
                    };

                    await _fieldAreaService.Create(entity);
                    return new DataResponse<FieldArea>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    return DataResponse<FieldArea>.False("Error", new string[] { ex.Message });
                }
            }
            return DataResponse<FieldArea>.False("Some properties are not valid", ModelStateError);
        }

        [HttpPost("Edit")]
        public async Task<DataResponse<FieldArea>> Edit([FromBody] FieldAreaEditVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _fieldAreaService.GetById(model.Id);
                    if (entity == null)
                        return DataResponse<FieldArea>.False("FieldArea not found");
                    entity.Name = model.Name;
                    entity.Description = model.Description;

                    await _fieldAreaService.Update(entity);
                    return new DataResponse<FieldArea>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    DataResponse<FieldArea>.False(ex.Message);
                }
            }
            return DataResponse<FieldArea>.False("Some properties are not valid", ModelStateError);
        }

        [HttpPost("GetData")]
        public DataResponse<PagedList<FieldAreaDto>> GetData([FromBody] FieldAreaSearch search)
        {
            return _fieldAreaService.GetData(search);
        }

        [HttpGet("GetAll")]
        public async Task<DataResponse<List<FieldArea>>> GetAll()
        {
            var data = await _fieldAreaService.GetQueryable().ToListAsync();

            return new DataResponse<List<FieldArea>>
            {
                Success = true,
                Message = "Success",
                Data = data,
            };
        }

        [HttpPost("Delete/{id}")]
        public async Task<DataResponse> Delete(Guid id)
        {
            try
            {
                var entity = _fieldAreaService.GetById(id);
                await _fieldAreaService.Delete(entity);
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


        [HttpGet("GetFieldArea/{id}")]
        public DataResponse<FieldAreaDto> GetFieldArea(Guid id)
        {
            return _fieldAreaService.GetDto(id);
        }

    }
}