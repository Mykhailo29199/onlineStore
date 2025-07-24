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

        public async Task CreateGameAsync (GameModel gameModel)
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
            var exists = await _unitOfWork.PlatformRepository.CheckIfExistAsync(platformId);
            if (!exists)
            {
                throw new PlatformNotFoundException(platformId); // <-- твій кастомний ексепшн
            }

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

        public async Task<IList<BaseGameModel>> GetGamesByGenreIdAsync(Guid genreId)
        {

            var exists = await _unitOfWork.GenreRepository.CheckIfExistAsync(genreId);
            if (!exists)
            {
                throw new GenreNotFoundException(genreId); // <-- теж кастомний ексепшн
            }

            var gameIds = await _unitOfWork.GameGenreRepository.GetGameIdsByGenreIdAsync(genreId);
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

        public async Task UpdateGameAsync(GameModel gameModel)
        {
            try
            {
                var gameId = gameModel.Game.Id;
                var gameKey = gameModel.Game.Key;

                GameEntity? existingGame = null;

                // 1. Перевірка наявності за Id
                if (gameId != Guid.Empty)
                {
                    existingGame = await _unitOfWork.GameRepository.GetByIdAsync(gameId);
                }

                // 2. Якщо не знайшли за Id — шукаємо за Key
                if (existingGame == null && !string.IsNullOrWhiteSpace(gameKey))
                {
                    existingGame = await _unitOfWork.GameRepository.GetGameByKeyAsync(gameKey);
                }

                // 3. Якщо все ще null — гра не існує
                if (existingGame == null)
                {
                    throw new GameNotExistException();
                }

                var actualGameId = existingGame.Id;

                // 4. Оновлення властивостей гри
                existingGame.Name = gameModel.Game.Name;
                // TODO: Add validation for Key




                gameModel.Game.Key = GenerateGameKeyIfMissing(gameModel.Game.Key, gameModel.Game.Name);

                if (!(await _unitOfWork.GameRepository.CheckIfKeyUniqueAsync(gameModel.Game.Key)))
                {
                    throw new GameKeyNotUniqueException(gameModel.Game.Key);
                }





                    existingGame.GameKey = gameModel.Game.Key;
                existingGame.Description = gameModel.Game.Description;

                // 5. Оновлення зв’язків з жанрами
                await _unitOfWork.GameGenreRepository.DeleteByGameIdAsync(actualGameId);
                foreach (var genreId in gameModel.Genres)
                {
                    if (await _unitOfWork.GenreRepository.CheckIfExistAsync(genreId))
                    {
                        await _unitOfWork.GameGenreRepository.AddAsync(new GameGenreEntity
                        {
                            GameId = actualGameId,
                            GenreId = genreId
                        });
                    }
                    else
                    {
                        throw new Exception("Unexisting genre id");
                    }
                }

                // 6. Оновлення зв’язків з платформами
                await _unitOfWork.GamePlatformRepository.DeleteByGameIdAsync(actualGameId);
                foreach (var platformId in gameModel.Platforms)
                {
                    if (await _unitOfWork.PlatformRepository.CheckIfExistAsync(platformId))
                    {
                        await _unitOfWork.GamePlatformRepository.AddAsync(new GamePlatformEntity
                        {
                            GameId = actualGameId,
                            PlatformId = platformId
                        });
                    }
                    else
                    {
                        throw new Exception("Unexisting platform id");
                    }
                   
                }
                await _unitOfWork.GameRepository.UpdateAsync(existingGame);
            }
            catch (GameNotExistException)
            {
                _ = CreateGameAsync(gameModel);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteGameAsync(string gameKey)
        {

            var existingGame = await _unitOfWork.GameRepository.GetGameByKeyAsync(gameKey);

            if (existingGame == null)
            {
                throw new GameKeyNotFoundException(gameKey);
            }

            await _unitOfWork.GameRepository.DeleteAsync(existingGame);
            await _unitOfWork.SaveAsync();
        }
    }
}
