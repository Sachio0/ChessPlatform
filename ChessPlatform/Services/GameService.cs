using ChessPlatform.Web.Enums;
using ChessPlatform.Web.Models;
using ChessPlatform.Web.Services.Interfaces;
using ChessPlatform.Web.Statics;

namespace ChessPlatform.Web.Services
{
    public class GameService : BaseService, IGameService
    {
        public GameService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<T> GetAllAsync<T>()
        {
            return await SendAsync<T>(new RequestDto
            {
                apiType = ApiType.GET,
                Url = Url.GetInstance().GetUrl(ApiCall.Game) + "/" + ApiType.GET.ToString(),
                //AccessToken = token
            });
        }

        public async Task InsertOneAsync<T>(T dto)
        {
            await SendAsync<T>(new RequestDto
            {
                apiType = ApiType.POST,
                Url = Url.GetInstance().GetUrl(ApiCall.Game) + "/" + ApiType.POST.ToString(),
                Data = dto,
                //AccessToken = token
            });
        }
    }
}
