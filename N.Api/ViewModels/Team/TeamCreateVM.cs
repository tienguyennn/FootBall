using Microsoft.AspNetCore.Identity;

namespace N.Api.ViewModels
{
    public class TeamCreateVM
    {
        public string? Name { get; set; }
        public string? Level { get; set; }
        public string? Age { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
    }
}