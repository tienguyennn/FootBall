using N.Repository;
using Microsoft.EntityFrameworkCore;
using N.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N.Repository.NDirectoryRepository
{
    public class FieldRepository : Repository<Field>, IFieldRepository
    {
        public FieldRepository(DbContext context) : base(context)
        {
        }
    }
}
