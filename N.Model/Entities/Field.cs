using Microsoft.AspNetCore.Identity;

namespace N.Model.Entities
{
    public class Field : Entity
    {
        public Guid? UserId { get; set; }
        public Guid? StaffId { get; set; }
        public Guid? FieldAreaId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Picture { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }
        public float? Price { get; set; }
        public string? Reason { get; set; }
    }
}
