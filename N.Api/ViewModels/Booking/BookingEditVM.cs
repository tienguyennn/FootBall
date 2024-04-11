using Microsoft.AspNetCore.Identity;

namespace N.Api.ViewModels
{
    public class BookingEditVM
    {
        public Guid? Id { get; set; }
        public Guid? FieldId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string? Description { get; set; }
        public List<Guid>? Services { get; set; }
    }
}
