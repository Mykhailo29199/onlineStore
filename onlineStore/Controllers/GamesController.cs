using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Context;
using Store.DataAccess.Entities;
using Store.WebAPI.DTOs;
using Store.Services.Services.Interfaces;
using Store.Services.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Store.Services.Services;

namespace onlineStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GamesController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] GameCreateDTO requestPostDTO)
        {
            if (requestPostDTO == null || requestPostDTO.Game == null)
            {
                return BadRequest("Invalid request body.");
            }
            var gameModel = _mapper.Map<GameModel>(requestPostDTO);
            await _gameService.CreateGameAsync(gameModel);

            // якщо GameKey не передано, згенеруЇмо його
            return NoContent();
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetGameByKey([FromRoute] string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return BadRequest("Game key must be provided.");
            }


            var gameModel = await _gameService.GetGameByKeyAsync(key);
            var responseDTO = _mapper.Map<BaseGameWithIdDTO>(gameModel);
            return Ok(responseDTO);
        }

        [HttpGet("find/{id}")]
        public async Task<IActionResult> GetGameById([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Game Id must be provided.");
            }

            var gameModel = await _gameService.GetGameByIdAsync(id);
            var responseDTO = _mapper.Map<BaseGameWithIdDTO>(gameModel);
            return Ok(responseDTO);
        }

        [HttpGet("platforms/{platformId}/games")]
        public async Task<IActionResult> GetGameByPlatformId([FromRoute] Guid platformId)
        {
            if (platformId == Guid.Empty)
            {
                return BadRequest("Platform Id must be provided.");
            }
            var gameModel = await _gameService.GetGamesByPlatformIdAsync(platformId);
            var responseDTO = _mapper.Map<IList<BaseGameWithIdDTO>>(gameModel);
            return Ok(responseDTO);
        }

        [HttpGet("genres/{ganreId}/games")]
        public async Task<IActionResult> GetGameByGanreId([FromRoute] Guid ganreId)
        {
            if (ganreId == Guid.Empty)
            {
                return BadRequest("Ganre Id must be provided.");
            }
            var gameModel = await _gameService.GetGamesByGenreIdAsync(ganreId);
            var responseDTO = _mapper.Map<IList<BaseGameWithIdDTO>>(gameModel);
            return Ok(responseDTO);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGame([FromBody] GamePutRequiredDTO requestPutDTO)
        {
            if (requestPutDTO == null || requestPutDTO.Game == null)
            {
                return BadRequest("Invalid request body.");
            }
            var gameModel = _mapper.Map<GameModel>(requestPutDTO);
            await _gameService.UpdateGameAsync(gameModel);

            return NoContent();
        }


        [HttpDelete("{gameKey}")]
        public async Task<IActionResult> DeleteGame([FromRoute] string gameKey)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                return BadRequest("Game key must be provided.");
            }


            await _gameService.DeleteGameAsync(gameKey);

            return NoContent(); // 204 Ч усп≥шно, але без т≥ла в≥дпов≥д≥
        }
    }

}