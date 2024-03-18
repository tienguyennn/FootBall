using Microsoft.AspNetCore.Identity;

namespace N.Api.ViewModels
{
    public class FeeCreateVM
    {
        public Guid? FieldId { get; set; }
        public string? Description { get; set; }
        public float? Price { get;  set; }
        public string? Name { get;  set; }
    }
}
