using Microsoft.EntityFrameworkCore;
using N.Model.Entities;


namespace N.Repository.FieldServiceFeeRepository
{
    public class FieldServiceFeeRepository : Repository<FieldServiceFee>, IFieldServiceFeeRepository
    {
        public FieldServiceFeeRepository(DbContext context) : base(context)
        {
        }
    }
}
