using Generator.Attributes;
using TaskManager.Domain.Abstractions;

namespace TaskManager.Domain;

[EfConstructor]
public partial class Card : Entity
{
    [RelationId(RelationType = typeof(User))]
    private int _userId;

    private Status _status;
    
    public Card(int userId, string name, string description, DateTime estimatedCompletionDate)
    {
        UserId = userId;
        Name = name;
        Description = description;
        EstimatedCompletionDate = estimatedCompletionDate;
        
        CreationDate = DateTime.UtcNow;
        _status = Status.Created;
    }
    
    public string Name { get; init; }
    
    public string Description { get; init; }
    
    public DateTime CreationDate { get; init; }
    
    public DateTime? ClosingDate { get; set; }

    public Status Status => _status;

    public DateTime EstimatedCompletionDate { get; set; }
    
    public long? ActualProcessingTime { get; set; }

    public void StartProcessing()
    {
        if (_status != Status.Created)
        {
            throw new Exception("Task was already started or completed");
        }
        
        _status = Status.Processing;
    }

    public void Complete()
    {
        if (_status == Status.Completed)
        {
            throw new Exception("Task was already completed");
        }
        
        _status = Status.Completed;
        ClosingDate = DateTime.UtcNow;
        ActualProcessingTime = ClosingDate.Value.Ticks - CreationDate.Ticks;
    }
}

public enum Status
{
    Created = 0,
    Processing = 1,
    Completed = 2
}