namespace ChessPlatform.Web.Services.Interfaces
{
    public interface IGameService
    {
        Task<T> GetAllAsync<T>();
        Task InsertOneAsync<T>(T dto);
    }
}
