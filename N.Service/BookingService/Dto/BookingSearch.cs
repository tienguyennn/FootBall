using N.Service.Common;

namespace N.Service.BookingService.Dto
{
    public class BookingSearch : SearchBase
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
