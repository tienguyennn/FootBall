using Microsoft.AspNetCore.Identity;

namespace N.Api.ViewModels
{
    public class TeamEditVM
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Level { get; set; }
        public string? Age { get; set; }
        public int Status { get; set; }
        public string? Phone { get; set; }
        public Guid? FieldId { get; set; }
        public string? Description { get; set; }

    }
}