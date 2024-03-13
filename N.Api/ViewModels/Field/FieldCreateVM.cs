using Microsoft.AspNetCore.Identity;

namespace N.Api.ViewModels
{
    public class FieldCreateVM 
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public IFormFile? Picture { get; set; }
        public float? Price { get; set; }
    }
}
