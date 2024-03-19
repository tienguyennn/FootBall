using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using N.Api.ViewModels;
using N.Model.Entities;
using N.Service.BookingService.Dto;
using N.Service.Common;
using N.Service.Constant;
using N.Service.DTO;
using N.Service.FieladService;
using N.Service.FieldService.Dto;
//using PagedList;

namespace N.Controllers
{
    [Route("api/[controller]")]
    public class FieldController : NController
    {
        private readonly IFieldService _fieldService;
        private readonly IMapper _mapper;
        private readonly ILogger<FieldController> _logger;


        public FieldController(
            IFieldService fieldService,
            IMapper mapper,
            ILogger<FieldController> logger
            )
        {
            this._fieldService = fieldService;
            this._mapper = mapper;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<DataResponse<Field>> Create([FromForm] FieldCreateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = new Field()
                    {
                        Address = model.Address,
                        Description = model.Description,
                        Name = model.Name,
                        Price = model.Price,
                        UserId = UserId,
                        Status = FieldStatusConstant.Pending,
                    };
                    if (model.Picture != null && model.Picture.Length > 0)
                    {
                        var upload = await UploadFile.Save(model.Picture);
                        if (upload.Success)
                        {
                            entity.Picture = upload.Path;
                        }
                    }


                  await  _fieldService.Create(entity);
                    return new DataResponse<Field>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    return DataResponse<Field>.False("Error", new string[] { ex.Message });
                }
            }
            return DataResponse<Field>.False("Some properties are not valid", ModelStateError);
        }

        [HttpPost("Edit")]
        public async Task<DataResponse<Field>> Edit([FromForm] FieldEditVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _fieldService.GetById(model.Id);
                    if (entity == null)
                        return DataResponse<Field>.False("Field not found");
                    entity.Name = model.Name;
                    entity.Description = model.Description;
                    entity.Address = model.Address;
                    entity.Price = model.Price;
                    if (model.Picture != null && model.Picture.Length > 0)
                    {
                        var upload = await UploadFile.Save(model.Picture);
                        if (upload.Success)
                        {
                            entity.Picture = upload.Path;
                        }
                    }
                   await _fieldService.Update(entity);
                    return new DataResponse<Field>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    DataResponse<Field>.False(ex.Message);
                }
            }
            return DataResponse<Field>.False("Some properties are not valid", ModelStateError);
        }

        [HttpPost("UpdateStatus")]
        public async Task<DataResponse<Field>> UpdateStatus([FromBody] FieldUpdateStatusVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _fieldService.GetById(model.Id);
                    if (entity == null)
                        return DataResponse<Field>.False("Field not found");
                    entity.Status = model.Status;
                    await _fieldService.Update(entity);
                    return new DataResponse<Field>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    DataResponse<Field>.False(ex.Message);
                }
            }
            return DataResponse<Field>.False("Some properties are not valid", ModelStateError);
        }


        [HttpPost("GetData")]
        public DataResponse<PagedList<FieldDto>> GetData([FromBody] FieldSearch search)
        {
            return _fieldService.GetData(search);
        }


        [HttpGet("GetField/{id}")]
        public DataResponse<FieldDto> GetField(Guid id)
        {
            return _fieldService.GetDto(id);
        }


        [HttpPost("GetFieldTimes")]
        public DataResponse<List<FieldTime>> GetFieldTimes(FieldTimeSearch search)
        {
            return _fieldService.GetFieldTimes(search);
        }

        [HttpGet("GetFees/{id}")]
        public DataResponse<List<Fee>> GetFees(Guid id)
        {
            return _fieldService.GetFees(id);
        }

    }
}