using ChessPlatform.Web.Enums;
using ChessPlatform.Web.Models;
using ChessPlatform.Web.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace ChessPlatform.Web.Services
{
    public class BaseService : IBaseService
    {
        protected ResponseDto ResponseModel { get; set; }
        protected IHttpClientFactory HttpClient { get; set; }
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            HttpClient = httpClientFactory;
            ResponseModel = new ResponseDto();
        }
        public async Task<T> SendAsync<T>(RequestDto apiRequest)
        {
            try
            {
                var client = HttpClient.CreateClient("GameAPI");
                var message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }
                //if (!string.IsNullOrWhiteSpace(apiRequest.AccessToken))
                //{
                //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
                //}
                switch (apiRequest.apiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                var apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(apiContent);
            }
            catch (Exception e)
            {
                var dto = new ResponseDto
                {
                    DisplayMessage = "Error",
                    ErrorMessage = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false,
                };
                var res = JsonConvert.SerializeObject(dto);
                return JsonConvert.DeserializeObject<T>(res);

            }
        }
    }
}
