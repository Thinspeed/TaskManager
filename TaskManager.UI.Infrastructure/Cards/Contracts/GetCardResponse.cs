using TaskManager.UI.Infrastructure.Shared.Contracts;

namespace TaskManager.UI.Models.Cards;

public class GetCardResponse
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public DateTime? ClosingDate { get; set; }
    
    public long? ActualProcessingTime { get; set; }
    
    public DateTime EstimatedCompletionDate { get; set; }
    
    public int Status { get; set; }

    public GetUserResponse User { get; set; }
}