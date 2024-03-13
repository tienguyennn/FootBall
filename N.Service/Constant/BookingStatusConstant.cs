using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N.Service.Constant
{
    public class BookingStatusConstant
    {
        public static string Confirm => "Admin";
        public static string Wait => "Staff";
        public static string Cancel => "FieldOwner";
    }
}
