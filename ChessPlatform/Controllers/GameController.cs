using ChessPlatform.Web.Models;
using ChessPlatform.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChessPlatform.Web.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        [HttpPost]
        public async Task Post(GameDto dto)
        {
            await _gameService.InsertOneAsync(dto);
        }
    }
}
