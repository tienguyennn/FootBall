using Microsoft.AspNetCore.Identity;

namespace N.Api.ViewModels
{
    public class FieldAreaEditVM
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

    }
}