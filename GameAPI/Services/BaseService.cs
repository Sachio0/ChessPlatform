using AutoMapper;
using GameAPI.Models;
using GameAPI.Repository;
using MongoDB.Driver;

namespace GameAPI.Services
{
    public class BaseService<T, TDto> : IBaseService<T, TDto> where TDto : class where T : class, IId
    {
        protected readonly IGenericRepository<T> Repository;
        protected readonly IMapper Mapper;
        public BaseService(MongoClientBase client, IMapper mapper)
        {
            Repository = new GenericRepository<T>(client);
            Mapper = mapper;
        }
        public async Task<List<TDto>> GetAsync(Predicate<T> predicate = null, int count = 0)
        {
            var res =  await Repository.GetAsync(predicate, count);
            return Mapper.Map<List<TDto>>(res);
        }

        public async Task InsertManyAsync(ICollection<TDto> model)
        {
            await Repository.InsertManyAsync(Mapper.Map<ICollection<T>>(model));
        }

        public async Task InsertOneAsync(TDto model)
        {
            await Repository.InsertOneAsync(Mapper.Map<T>(model));
        }
    }
}
