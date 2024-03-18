using N.Model.Entities;
using N.Service.BookingService.Dto;
using N.Service.Common.Service;
using N.Service.DTO;
using N.Service.FieldService.Dto;

namespace N.Service.BookingService
{
    public interface IBookingService : IService<Booking>
    {
        DataResponse<List<BookingDto>> History(BookingSearch search, Guid? userId);
    }
}
