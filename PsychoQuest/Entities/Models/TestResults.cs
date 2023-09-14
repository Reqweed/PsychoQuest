using System.ComponentModel.DataAnnotations;
using Entities.Enums;

namespace Entities.Models;

public class TestResults
{
    public long TestResultsId { get; set; }
    
    public long UserId { get; set; }
    
    public TypeTest TestName { get; set; }
    
    [Required]
    public string Result { get; set; } = string.Empty;
    
    public long Points { get; set; }
}