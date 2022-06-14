using AutoMapper;
using GameAPI.Dto;
using GameAPI.Models;
using GameAPI.Services.Interfaces;
using MongoDB.Driver;

namespace GameAPI.Services
{
    public class GameService : BaseService<Game,GameDto>, IGameService
    {
        public GameService(MongoClientBase client, IMapper mapper) : base(client, mapper)
        {
        }

    }
}
