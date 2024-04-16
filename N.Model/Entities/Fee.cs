using Microsoft.AspNetCore.Identity;

namespace N.Model.Entities
{
    public class ServiceFee : Entity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public float? Price { get; set; }
        public Guid? UserId { get; set; }
    }
}
