using Microsoft.AspNetCore.Identity;

namespace N.Api.ViewModels
{
    public class FeePaymentCreateVM
    {
        public Guid? BookingId { get; set; }
        public Guid? FeeId { get; set; }
        public DateTime? DateTime { get; set; }
        public float? Price { get; set; }
        public string? Description { get; set; }
    }
}
