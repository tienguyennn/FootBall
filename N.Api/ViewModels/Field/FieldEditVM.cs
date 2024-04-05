using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace N.Api.ViewModels
{
    public class FieldEditVM
    {
        public Guid? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public Guid? FieldAreaId { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }
        public float? Price { get;  set; }
        public IFormFile? Picture { get;  set; }
    }
}
