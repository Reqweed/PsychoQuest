namespace Entities.Models;

public class Question
{
    public string QuestionText { get; set; } = string.Empty;
    
    public List<string> Answers { get; set; } = new List<string>();
}