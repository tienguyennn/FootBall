using Microsoft.EntityFrameworkCore;
using N.Model.Entities;


namespace N.Repository.BookingRepository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(DbContext context) : base(context)
        {
        }
    }
}
