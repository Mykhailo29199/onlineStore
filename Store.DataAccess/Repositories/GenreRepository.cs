using Store.DataAccess.Context;
using Store.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories
{
    public class GenreRepository : GenericRepository<GenreEntity>
    {
        public GenreRepository(StoreContext context) : base(context)
        {
        }
    }
}
