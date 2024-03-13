using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace N.Api.ViewModels
{
    public class FieldCreateVM 
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public IFormFile? Picture { get; set; }
        public float? Price { get; set; }
    }
}
