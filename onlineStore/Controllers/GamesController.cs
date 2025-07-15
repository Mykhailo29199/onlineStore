using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Context;
using Store.DataAccess.Entities;
using Store.WebAPI.DTOs;
using Store.Services.Services.Interfaces;
using Store.Services.Models;
using AutoMapper;

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

        [HttpPost("games")]
        public async Task<IActionResult> CreateGame([FromBody] GameCreateRequestDTO requestPostDTO)
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

        [HttpGet("games/{key}")]
        public async Task<IActionResult> GetGameByKey([FromRoute] string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return BadRequest("Game key must be provided.");
            }


            var gameModel = await _gameService.GetGameByKeyAsync(key);
            var responseDTO = _mapper.Map<GameGetByKeyDTO>(gameModel);
            return Ok(responseDTO);
        }
    }
}
