using N.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N.Service.FieldService.Dto
{
    public class FieldTime 
    {
        public Guid? FieldId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Booked { get; set; }
        public bool Expired { get; set; }

    }
}
