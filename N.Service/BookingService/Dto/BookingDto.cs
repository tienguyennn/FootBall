using N.Model.Entities;
using N.Service.Dto;
using N.Service.FieldService.Dto;
using N.Service.ServiceFeePaymentService.Dto;

namespace N.Service.BookingService.Dto
{
    public class BookingDto : Booking
    {
        public AppUserDto? User { get; set; }
        public Field? Field { get; set; }

        public List<ServiceFeePaymentDto>? Services { get; set; }
    }
}
