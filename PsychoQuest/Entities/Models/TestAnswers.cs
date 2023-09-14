using Entities.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models;

public class TestAnswers
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string TestAnswersId { get; set; } = string.Empty;
    
    public long UserId { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public TypeTest TestName { get; set; }
    
    public List<int> Answers { get; set; } = new List<int>();
}