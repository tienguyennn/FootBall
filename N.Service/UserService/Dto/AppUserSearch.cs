using N.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N.Service.UserService.Dto
{
    public class AppUserSearch : SearchBase
    {
        public string? Type { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
    }
}
