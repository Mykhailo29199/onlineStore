using Store.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Services.Interfaces
{
    public interface IGameService
    {
        Task CreateGameAsync(GameModel gameModel);
        Task<BaseGameModel> GetGameByKeyAsync(string gameKey);
        Task<BaseGameModel> GetGameByIdAsync(Guid gameId);
        Task<IList<BaseGameModel>> GetGamesByPlatformIdAsync(Guid platformId);
        Task<IList<BaseGameModel>> GetGamesByGenreIdAsync(Guid genreId);
        Task UpdateGameAsync(GameModel model);
        Task DeleteGameAsync(string gameKey);
    }
}
