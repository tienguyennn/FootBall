using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using N.Api.ViewModels;
using N.Model.Entities;
using N.Service.Common;
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
        public DataResponse<Field> Create([FromBody] FieldCreateVM model)
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
                    };
                    _fieldService.Create(entity);
                    return new DataResponse<Field>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                   return DataResponse<Field>.False("Error", new string[] { ex.Message });
                }
            }
            return DataResponse<Field>.False("Some properties are not valid", ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)));
        }

        [HttpPost("Edit")]
        public DataResponse<Field> Edit([FromBody] FieldEditVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _fieldService.GetById(model.Id);
                    if(entity == null)
                        return DataResponse<Field>.False("Field not found");
                    entity.Name = model.Name;
                    entity.Description = model.Description;
                    entity.Address = model.Address;
                    entity.Price = model.Price;
                    _fieldService.Update(entity);
                    return new DataResponse<Field>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    DataResponse<Field>.False(ex.Message);
                }
            }
            return DataResponse<Field>.False("Some properties are not valid", ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)));
        }

        [HttpPost("GetData")]
        public DataResponse<PagedList<FieldDto>> GetData([FromBody] FieldSearch search)
        {
            return _fieldService.GetData(search);
        }
    }
}