using AutoMapper;
using Store.DataAccess.Context;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories;
using Store.DataAccess.UnitOfWork;
using Store.Services.Models;
using Store.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork<StoreContext> _unitOfWork;
        private readonly GameRepository _gameRepository;
        private IMapper _mapper;

        public GameService(IUnitOfWork<StoreContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //public void CreateGame(GameModel gameModel)
        //{
        //    GameEntity gameEntity = _mapper.Map<GameEntity>(gameModel);
        //    _unitOfWork.GameRepository.Add(gameEntity);
        //    _unitOfWork.Save();
        //}

        public async Task CreateGameAsync(GameModel gameModel)
        {
            // спочатку поробити перевірки через модельки, а не ентіті, а потім помапити

            GameEntity gameEntity = _mapper.Map<GameEntity>(gameModel);

            gameEntity.GameKey = GenerateGameKeyIfMissing(gameEntity.GameKey, gameEntity.Name);

            if(!(await _unitOfWork.GameRepository.CheckIfKeyUniqueAsync(gameEntity.GameKey)))
            {
                throw new Exception();
            }

            foreach (var platform in gameEntity.GamePlatforms) 
            {
               if( await _unitOfWork.PlatformRepository.CheckIfExistAsync(platform.PlatformId))
                {
                    throw new Exception(); // dorobutu
                }

            }

            foreach (var genre in gameEntity.GameGenres)
            {
                if (await _unitOfWork.GenreRepository.CheckIfExistAsync(genre.GenreId))
                {
                    throw new Exception();
                }

            }




            await _unitOfWork.GameRepository.AddAsync(gameEntity);
            await _unitOfWork.SaveAsync();
        }

        private string GenerateGameKeyIfMissing(string gameKey, string gameName)
        {
            if (
                string.IsNullOrWhiteSpace(gameKey) || string.Empty == gameKey
               )
            {
                if(string.IsNullOrWhiteSpace(gameName) || string.Empty == gameName)
                {
                    throw new Exception("No name");
                }

                gameKey = gameName
                    .ToLower()
                    .Replace(" ", "-")
                    .Replace(".", "")
                    .Replace(",", "")
                    .Replace(":", "")
                    .Replace(";", "")
                    .Replace("!", "")
                    .Replace("?", "");
            }

            return gameKey;

        }
    }
}
