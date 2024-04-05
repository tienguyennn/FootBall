using N.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N.Service.FieldService.Dto
{
    public class FieldSearch : SearchBase
    {
        public Guid? FieldAreaId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? StaffId { get; set; }
        public string? Status { get; set; }
    }
}
