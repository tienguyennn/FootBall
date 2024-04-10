using Microsoft.AspNetCore.Identity;

namespace N.Api.ViewModels
{
    public class FieldUpdateStatusVM
    {
        public Guid? Id { get; set; }
        public string? Status { get; set; }
        public string? Reason { get; set; }
    }
}
