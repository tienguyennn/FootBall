using N.Service.Common;

namespace N.Service.BookingService.Dto
{
    public class BookingSearch : SearchBase
    {
        public Guid? UserId { get; set; }
        public Guid? FieldId { get; set; }
        public Guid? OwnerId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public bool? Deposited { get; set; }
        public bool? Paid { get; set; }
    }
}
