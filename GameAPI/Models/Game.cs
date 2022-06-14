using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameAPI.Models
{
    
    public class Game : IId
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string White { get; set; }
        public string Black { get; set; }
        public string PGN { get; set; }
    }
}
