using System.ComponentModel.DataAnnotations;

namespace N.Api.ViewModels.Account
{
    public class SetStaffVM
    {
        public Guid? FieldOwnerId { get; set; }
        public Guid? StaffId { get; set; }
    }
}
