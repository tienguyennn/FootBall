using Microsoft.AspNetCore.Identity;

namespace N.Api.ViewModels
{
    public class ServiceFeePaymentCreateVM
    {
        public Guid? BookingId { get; set; }
        public Guid? FieldServiceFeeId { get; set; }
        public DateTime? DateTime { get; set; }
        public float? Price { get; set; }
        public string? Description { get; set; }
    }
}
