using Microsoft.AspNetCore.Identity;

namespace N.Api.ViewModels
{
    public class ServiceFeeCreateVM
    {
        public string? Description { get; set; }
        public string? Name { get;  set; }
        public float? Price { get;  set; }
        public string? Icon { get;  set; }
    }
}
