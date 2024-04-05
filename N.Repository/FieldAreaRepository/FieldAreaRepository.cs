using Microsoft.EntityFrameworkCore;
using N.Model.Entities;


namespace N.Repository.FieldAreaRepository
{
    public class FieldAreaRepository : Repository<FieldArea>, IFieldAreaRepository
    {
        public FieldAreaRepository(DbContext context) : base(context)
        {
        }
    }
}
