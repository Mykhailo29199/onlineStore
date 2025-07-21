using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Context;
using Store.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories
{
    public class GamePlatformRepository : GenericRepository<PlatformEntity>
    {
        public readonly DbSet<GamePlatformEntity> _dbSet;
        public GamePlatformRepository(StoreContext context) : base(context) 
        {
            _dbSet = context.Set<GamePlatformEntity>();
        }

        public async Task<IEnumerable<Guid>> GetGameIdsByPlatformIdAsync(Guid platformId)
        {
            return await _dbSet.Where(x => x.PlatformId == platformId).Select(x => x.GameId).ToListAsync();
        }
    }
}
