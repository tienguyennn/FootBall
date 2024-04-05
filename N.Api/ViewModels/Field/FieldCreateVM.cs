using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace N.Api.ViewModels
{
    public class FieldCreateVM 
    {
        [Required]
        public Guid? FieldAreaId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public IFormFile? Picture { get; set; }
        public float? Price { get; set; }
        public List<FieldServiceFreeCreate>? Services { get; set; }
    }

    public class FieldServiceFreeCreate
    {
        public Guid? ServiceFeeId { get; set; }
        public float? Price { get; set; }
    }
}
