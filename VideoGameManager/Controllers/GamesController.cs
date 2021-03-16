using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameManager.DataAccess;

namespace VideoGameManager.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GamesController : ControllerBase
    {
        private readonly VideoGameDataContext _context;

        public GamesController(VideoGameDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Game> GetAllGames() => _context.Games;

        [HttpPost]
        public async Task<Game> AddGame([FromBody]Game newGame)
        {
            _context.Add(newGame);
            await _context.SaveChangesAsync();
            return newGame;
        }
    }
}
