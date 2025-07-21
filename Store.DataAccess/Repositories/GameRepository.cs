using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Context;
using Store.DataAccess.Entities;
using Store.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories
{
    public class GameRepository : GenericRepository<GameEntity>
    {
        private readonly DbSet<GameEntity> _dbSet;

        public GameRepository(StoreContext context) : base(context)
        {
            _dbSet = context.Set<GameEntity>();
        }

        public async Task<bool> CheckIfKeyUniqueAsync(string gameKey)
        {
            if (await _dbSet.AnyAsync(x => x.GameKey == gameKey))
            {
                return false;
            }
            return true;

        }

        public async Task<GameEntity> GetGameByKeyAsync(string gameKey)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.GameKey == gameKey);
        }

    }
}
