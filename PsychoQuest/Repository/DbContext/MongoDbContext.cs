using Entities.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Repository.DbContext;

public class MongoDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private const string QuestionsDirectory = "TestQuestions";
    
    private readonly IMongoDatabase _database;
    private readonly IConfiguration _configuration;

    public MongoDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        var client = new MongoClient(_configuration.GetSection("MongoDb:ConnectionString").Value);
        _database = client.GetDatabase(_configuration.GetSection("MongoDb:DatabaseName").Value);
        
        _database.CreateCollection(_configuration.GetSection("MongoDb:Collection_answers").Value);
        _database.CreateCollection(_configuration.GetSection("MongoDb:Collection_questions").Value);
        
        InitializeTestQuestionsFromJsonFile(QuestionsDirectory);
    }

    public IMongoCollection<TestAnswers> TestAnswers => _database.GetCollection<TestAnswers>(_configuration.GetSection("MongoDb:Collection_answers").Value);
    public IMongoCollection<TestQuestions> TestQuestions => _database.GetCollection<TestQuestions>(_configuration.GetSection("MongoDb:Collection_questions").Value);

    private void InitializeTestQuestionsFromJsonFile(string questionsDirectory)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        
        var folderPath = Path.Combine(currentDirectory, questionsDirectory);
        
        string[] filePaths = Directory.GetFiles(folderPath);

        foreach (var filePath in filePaths)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var filter = Builders<TestQuestions>.Filter.And(
                Builders<TestQuestions>.Filter.Eq(nameof(Entities.Models.TestQuestions.TestName), fileName)
            );
            var questions = this.TestQuestions.Find(filter).FirstOrDefault();
        
            if(questions is null)
            {
                var jsonData = File.ReadAllText(filePath);
                    
                var questionData = JsonConvert.DeserializeObject<TestQuestions>(jsonData);
                    
                this.TestQuestions.InsertOne(questionData!);
            }
        }
    }
}