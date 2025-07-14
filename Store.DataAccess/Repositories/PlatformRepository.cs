using Store.DataAccess.Context;
using Store.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories
{
    public class PlatformRepository : GenericRepository<PlatformEntity>
    {
        public PlatformRepository(StoreContext context) : base(context) 
        { 
            
        }
    }
}
