namespace TaskManager.UI.Infrastructure.Cards.Contracts;

public class CreateCardRequest
{
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime EstimatedCompletionDate { get; set; }
}