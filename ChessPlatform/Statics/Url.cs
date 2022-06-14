using ChessPlatform.Web.Enums;

namespace ChessPlatform.Web.Statics
{
    public class Url
    {
        private static Url _instance;
        private Dictionary<ApiCall,string> _calls;
        private Url() { }
        public static Url GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Url();
            }
            return _instance;
        }
        public void AddCall(ApiCall call, string url)
        {
            if(_calls == null) _calls = new Dictionary<ApiCall,string>();
            if (_calls.Keys.Contains(call)) _calls.Add(call, url);
            else _calls[call] = url;
        }
        public string GetUrl(ApiCall call) => _calls[call];
    }
}
