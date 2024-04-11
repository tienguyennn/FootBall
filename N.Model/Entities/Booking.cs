using Microsoft.AspNetCore.Identity;

namespace N.Model.Entities
{
    public class Booking : Entity
    {
        public Guid? FieldId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? DateTime { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public float? Price { get; set; }
        public float? DepositValue { get; set; }
        public bool Deposited { get; set; }
        public bool Paid { get; set; }
    }
}
