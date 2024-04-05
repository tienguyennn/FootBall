using Microsoft.AspNetCore.Identity;

namespace N.Model.Entities
{
    public class FieldArea : Entity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
