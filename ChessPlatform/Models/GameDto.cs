namespace ChessPlatform.Web.Models
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public string White { get; set; }
        public string Black { get; set; }
        public string PGN { get; set; }
    }
}
