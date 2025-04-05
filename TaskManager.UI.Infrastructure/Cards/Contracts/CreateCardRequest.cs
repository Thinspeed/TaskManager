namespace TaskManager.UI.Infrastructure.Cards.Contracts;

public class CreateCardRequest
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    DateTime EstimatedCompletionDate { get; set; }
}