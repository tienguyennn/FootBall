using N.Model.Entities;
using N.Service.FieldService.Dto;

namespace N.Service.FieldAreaService.Dto
{
    public class FieldAreaDto : FieldArea
    {
        public List<FieldDto>? Fields { get; set; }
    }
}
