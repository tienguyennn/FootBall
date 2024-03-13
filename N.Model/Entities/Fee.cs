using Microsoft.AspNetCore.Identity;

namespace N.Model.Entities
{
    public class Fee : Entity
    {
        public Guid? FieldId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public float? Price { get; set; }
    }
}
