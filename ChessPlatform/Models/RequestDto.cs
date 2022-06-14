using ChessPlatform.Web.Enums;

namespace ChessPlatform.Web.Models
{
    public class RequestDto
    {
        public ApiType apiType { get; set; } = ApiType.GET;

        //URL is Useless when you use Queue
        public string Url { get; set; }
        public object Data { get; set; }
        //public string AccessToken { get; set; }
    }
}
