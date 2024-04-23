using N.Model.Entities;
using N.Service.FieldServiceFeeService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N.Service.FieldService.Dto
{
    public class FieldDto : Field
    {
        public List<FieldTime>? FieldTimes { get; set; }
        public List<FieldServiceFeeDto>? Services { get; set; }
        public List<FieldArea>? FieldAreas { get; set; }
    }
}
