using GameAPI.Models;

namespace GameAPI.Services
{
    public interface IBaseService<T,TDto> where T : class, IId where TDto : class
    {
        public  Task InsertOneAsync(TDto model);
        public  Task InsertManyAsync(ICollection<TDto> model);
        public  Task<List<TDto>> GetAsync(Predicate<T> predicate = null, int count = 0);
    }
}
