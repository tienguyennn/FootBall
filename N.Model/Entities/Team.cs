using Microsoft.AspNetCore.Identity;

namespace N.Model.Entities
{
    public class Team : Entity
    {
        public Guid? UserId { get; set; }
        public string? Name { get; set; }
        public string? Level { get; set; }
        public string? Age { get; set; }
        public int Status { get; set; }
        public Guid? FieldId { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
    }
}
