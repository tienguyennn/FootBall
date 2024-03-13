using Microsoft.AspNetCore.Identity;

namespace N.Model.Entities
{
    public class FeePayment : Entity
    {
        public Guid? FeeId { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Description { get; set; }
        public float? Price { get; set; }
    }
}
