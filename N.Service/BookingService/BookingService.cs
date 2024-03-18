using N.Model.Entities;
using N.Repository.BookingRepository;
using N.Service.BookingService.Dto;
using N.Service.Common.Service;
using N.Service.DTO;
using N.Service.FieldService.Dto;

namespace N.Service.BookingService
{
    public class BookingService : Service<Booking>, IBookingService
    {
        public BookingService(
            IBookingRepository bookingRepository
            ) : base(bookingRepository)
        {
        }

        public DataResponse<List<BookingDto>> History(BookingSearch search, Guid? userId)
        {
            var query = from q in GetQueryable().Where(x => x.UserId == userId)
                        select new BookingDto()
                        {
                            Description = q.Description,
                            Id = q.Id,
                            UserId = q.UserId,
                            Price = q.Price,
                            Status = q.Status,
                            DateTime = q.DateTime,
                            End = q.End,
                            FieldId = q.FieldId,
                            Start = q.Start,
                        };

            var result = query.ToList();

            return new DataResponse<List<BookingDto>>()
            {
                Data = result,
                Success = true,
            };
        }
    }
}
