using N.Model.Entities;
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
    }
}
