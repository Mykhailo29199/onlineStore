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
    }
}
