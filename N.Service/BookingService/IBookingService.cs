using N.Model.Entities;
using N.Service.BookingService.Dto;
using N.Service.Common;
using N.Service.Common.Service;
using N.Service.DTO;
using N.Service.FieldService.Dto;

namespace N.Service.BookingService
{
    public interface IBookingService : IService<Booking>
    {
        DataResponse<PagedList<BookingDto>> History(BookingSearch search);
        DataResponse<BookingDto> GetDto(Guid id);
        bool CheckBooked(Guid? fieldId, DateTime? start, DateTime? end, Guid? id = null);
    }
}
