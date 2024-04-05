using Microsoft.AspNetCore.Identity;

namespace N.Api.ViewModels
{
    public class UpdateStatusVM
    {
        public Guid? Id { get; set; }
        public string? Status { get; set; }
    }
}
