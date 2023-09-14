using Entities.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models;

public class TestQuestions
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string TestQuestionsId { get; set; } = string.Empty;
    
    [BsonRepresentation(BsonType.String)]
    public TypeTest TestName { get; set; }
    
    public List<Question> Questions { get; set; } = new List<Question>();
}