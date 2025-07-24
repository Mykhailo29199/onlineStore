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
    public class GameGenreRepository : GenericRepository<GameGenreEntity>
    {
        public readonly DbSet<GameGenreEntity> _dbSet;
        public GameGenreRepository(StoreContext context) : base(context)
        {
            _dbSet = context.Set<GameGenreEntity>();
        }

        public async Task<IEnumerable<Guid>> GetGameIdsByGenreIdAsync(Guid genreId)
        {
            return await _dbSet.Where(x => x.GenreId == genreId).Select(x => x.GameId).ToListAsync();
        }
        public async Task DeleteByGameIdAsync(Guid gameId)
        {
            var records = await _dbSet.Where(x => x.GameId == gameId).ToListAsync();
            _dbSet.RemoveRange(records);
        }
    }
}
