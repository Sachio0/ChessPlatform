using GameAPI.Models;
using MongoDB.Driver;

namespace GameAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IId
    {
        public IMongoDatabase Database { get; }
        public GenericRepository(IMongoClient client)
        {
            Database = client.GetDatabase("mydb");
        }
        public async Task InsertOneAsync(T model)
        {
            var collection = Database.GetCollection<T>(typeof(T).Name);
            await collection.InsertOneAsync(model);
        }
        public async Task InsertManyAsync(ICollection<T> model)
        {
            var collection = Database.GetCollection<T>(typeof(T).Name);
            await collection.InsertManyAsync(model);
        }
        public async Task<List<T>> GetAsync(Predicate<T> predicate = null, int count = 0)
        {
            var collection = Database.GetCollection<T>(typeof(T).Name);
            if(predicate == null) return await collection.Find(_ =>true).ToListAsync();
            return await collection.Find(n => predicate(n)).ToListAsync();
        }
    }
}
