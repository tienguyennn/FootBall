using Microsoft.AspNetCore.Identity;

namespace N.Model.Entities
{
    public class FieldServiceFee : Entity
    {
        public Guid? ServiceFeeId { get; set; }
        public Guid? FieldId { get; set; }
        public float? Price { get; set; }
    }
}
