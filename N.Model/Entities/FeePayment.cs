using Microsoft.AspNetCore.Identity;

namespace N.Model.Entities
{
    public class ServiceFeePayment : Entity
    {
        public Guid? FieldServiceFeeId { get; set; }
        public Guid? BookingId { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Description { get; set; }
        public float? Price { get; set; }
    }
}
