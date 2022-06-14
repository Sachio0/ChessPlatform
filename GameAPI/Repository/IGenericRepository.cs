using GameAPI.Models;

namespace GameAPI.Repository
{
    public interface IGenericRepository<T> where T : class, IId
    {
        Task InsertOneAsync(T model);
        Task InsertManyAsync(ICollection<T> model);
        Task<List<T>> GetAsync(Predicate<T> predicate, int count = 0);
    }
}
