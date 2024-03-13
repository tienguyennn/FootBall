using N.Model.Entities;
using N.Repository.BookingRepository;
using N.Service.Common.Service;

namespace N.Service.BookingService
{
    public class BookingService : Service<Booking>, IBookingService
    {
        public BookingService(
            IBookingRepository bookingRepository
            ) : base(bookingRepository)
        {
        }


    }
}
