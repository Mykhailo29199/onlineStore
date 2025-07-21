using AutoMapper;
using Store.DataAccess.Context;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories;
using Store.DataAccess.UnitOfWork;
using Store.Services.Models;
using Store.Services.Services.Interfaces;
using OnlineStore.Shared.ExtensionShared;

namespace Store.Services.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork<StoreContext> _unitOfWork;
        private IMapper _mapper;

        public GameService(IUnitOfWork<StoreContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateGameAsync(GameModel gameModel)
        {
            // спочатку поробити перевірки через модельки, а не ентіті, а потім помапити

            GameEntity gameEntity = _mapper.Map<GameEntity>(gameModel);

            gameEntity.GameKey = GenerateGameKeyIfMissing(gameEntity.GameKey, gameEntity.Name);

            if (!(await _unitOfWork.GameRepository.CheckIfKeyUniqueAsync(gameEntity.GameKey)))
            {
                throw new GameKeyNotUniqueException(gameEntity.GameKey);
            }

            foreach (var platform in gameEntity.GamePlatforms)
            {
                if (await _unitOfWork.PlatformRepository.CheckIfExistAsync(platform.PlatformId))
                {
                    throw new PlatformNotFoundException(platform.PlatformId);
                }

            }

            foreach (var genre in gameEntity.GameGenres)
            {
                if (await _unitOfWork.GenreRepository.CheckIfExistAsync(genre.GenreId))
                {
                    throw new GenreNotFoundException(genre.GenreId);
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
                if (string.IsNullOrWhiteSpace(gameName) || string.Empty == gameName)
                {
                    throw new GameNoNameException();
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

        public async Task<BaseGameModel> GetGameByKeyAsync(string gameKey)
        {
            // Отримуємо гру з бази
            var gameEntity = await _unitOfWork.GameRepository.GetGameByKeyAsync(gameKey);

            // Якщо не знайдено — кидаємо інший виняток
            if (gameEntity == null)
            {
                throw new GameKeyNotFoundException(gameKey);
            }

            // Повертаємо мапінг в GameModel (а контролер уже мапить на DTO)
            return _mapper.Map<BaseGameModel>(gameEntity);
        }

        public async Task<BaseGameModel> GetGameByIdAsync(Guid gameId)
        {
            var gameEntity = await _unitOfWork.GameRepository.GetByIdAsync(gameId);
            if (gameEntity == null)
            {
                throw new GameIdNotFoundException(gameId);
            }
            return _mapper.Map<BaseGameModel>(gameEntity);
        }

        public async Task<IList<BaseGameModel>> GetGamesByPlatformIdAsync (Guid platformId)
        {
            var gameIds = await _unitOfWork.GamePlatformRepository.GetGameIdsByPlatformIdAsync(platformId);
            var result = new List<BaseGameModel>();

            foreach (var gameId in gameIds)
            {
                var gameModel = await GetGameByIdAsync(gameId);
                if (gameModel != null)
                {
                    result.Add(gameModel);
                }
            }
            return result;
        }
    }
}
