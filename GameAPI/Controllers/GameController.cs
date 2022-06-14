using GameAPI.Dto;
using GameAPI.Models;
using GameAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController( IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<List<GameDto>> Get()
        {
            return await _gameService.GetAsync();
        }
        [HttpPost]
        public async Task Post(GameDto dto)
        {
            await _gameService.InsertOneAsync(dto);
        }
        [HttpPost("~/InsertDummyData")]
        public async Task InsertDummyData()
        {
            var pgn = "1. e4 {[%clk 0:04:58.5][%c_effecte4;square;e4;type;Book;persistent;true][%c_highlighte2;color;%23a88865;opacity;0.5;square;e2;persistent;true,e4;color;%23a88865;opacity;0.5;square;e4;persistent;true]}1... d6 {[%clk 0:04:59.9]} 2. d4 {[%clk 0:04:46.3]} 2... Nf6 {[%clk 0:04:58.3]}3. Nc3 {[%clk 0:04:44.8]} 3... c6 {[%clk 0:04:57]} 4. Bd3 {[%clk 0:04:34.8]}4... g6 {[%clk 0:04:52.6]} 5. Nge2 {[%clk 0:04:30.9]} 5... Bg7 {[%clk0:04:51.7]} 6. O-O {[%clk 0:04:28.5]} 6... O-O {[%clk 0:04:51.3]} 7. Be3 {[%clk0:04:14.5]} 7... Bg4 {[%clk 0:04:49.5]} 8. Qd2 {[%clk 0:04:11.6]} 8... Qb6{[%clk 0:04:45.6]} 9. b3 {[%clk 0:04:07.8]} 9... Nbd7 {[%clk 0:04:42]} 10. h3{[%clk 0:04:06.8]} 10... Bxe2 {[%clk 0:04:39.5]} 11. Nxe2 {[%clk 0:04:01.1]}11... e5 {[%clk 0:04:30.4]} 12. d5 {[%clk 0:03:53.3]} 12... Qc7 {[%clk0:04:27.3]} 13. Rac1 {[%clk 0:03:46.7]} 13... Nc5 {[%clk 0:04:22.4]} 14. c4{[%clk 0:03:26.2]} 14... a6 {[%clk 0:04:14.3]} 15. Bxc5 {[%clk 0:03:18.4]} 15...dxc5 {[%clk 0:04:12.2]} 16. Qe3 {[%clk 0:03:07]} 16... b6 {[%clk 0:04:10]} 17.Kh1 {[%clk 0:02:38.8]} 17... Rfd8 {[%clk 0:04:08.1]} 18. Ng1 {[%clk 0:02:36.7]}18... b5 {[%clk 0:04:04.5]} 19. Qg3 {[%clk 0:02:27.6]} 19... bxc4 {[%clk0:04:02.4]} 20. bxc4 {[%clk 0:02:26.4]} 20... cxd5 {[%clk 0:04:00.3]} 21. exd5{[%clk 0:02:14]} 21... Rac8 {[%clk 0:03:53.1]} 22. Rfe1 {[%clk 0:02:09.2]} 22...e4 {[%clk 0:03:48.2]} 23. Bxe4 {[%clk 0:02:04.8]} 23... Qxg3 {[%clk 0:03:42.9]}24. fxg3 {[%clk 0:01:59.6]} 24... Nxe4 {[%clk 0:03:40.9]} 25. Rxe4 {[%clk0:01:59.5]} 25... Bd4 {[%clk 0:03:34.3]} 26. Nf3 {[%clk 0:01:56.7]} 26... Re8{[%clk 0:03:31.8]} 27. Rce1 {[%clk 0:01:52.3]} 27... Rxe4 {[%clk 0:03:23.5]} 28.Rxe4 {[%clk 0:01:51.3]} 28... Rb8 {[%clk 0:03:18.8]} 29. Nxd4 {[%clk 0:01:45]}29... cxd4 {[%clk 0:03:17.4]} 30. Rxd4 {[%clk 0:01:44.1]} 30... Rb1+ {[%clk0:03:16.9]} 31. Kh2 {[%clk 0:01:42.8]} 31... Rb2 {[%clk 0:03:16.2]} 32. d6{[%clk 0:01:37.9]} 32... Rb8 {[%clk 0:03:08.7]} 33. c5 {[%clk 0:01:36.3]} 33...Kf8 {[%clk 0:03:06.5]} 34. d7 {[%clk 0:01:35.3]} 34... Rd8 {[%clk 0:03:05.2]}35. c6 {[%clk 0:01:34.6]} 35... Ke7 {[%clk 0:03:04.1]} 36. g4 {[%clk 0:01:22.2]}36... Ke6 {[%clk 0:03:01.8]} 37. Kg3 {[%clk 0:01:21.6]} 37... Ke5 {[%clk0:03:00.7]} 38. Rd2 {[%clk 0:01:16.3]} 38... Ke4 {[%clk 0:02:50.8]} 39. c7{[%clk 0:01:15.5][%c_effectc7;square;c7;type;BestMove;persistent;true][%c_highlightc6;color;%239eba5a;opacity;0.5;square;c6;persistent;true,c7;color;%239eba5a;opacity;0.5;square;c7;persistent;true]<br /><br />Partia mogła być kontynuowana...} (39. c7 Rxd7 40. Rxd7 h5 41. c8=Qh4+ 42. Kf2 Ke5 {+M6}) 1-0";
            var dummy = new List<GameDto>
            {
                new GameDto
                {
                    Black = "Bl",
                    White = "Wh",
                    PGN =pgn
                },
                new GameDto
                {
                    Black = "B",
                    White = "W",
                    PGN =pgn
                },
                new GameDto
                {
                    Black = "Black",
                    White = "White",
                    PGN =pgn
                }
            };
             await _gameService.InsertManyAsync(dummy);
        }
    }
}