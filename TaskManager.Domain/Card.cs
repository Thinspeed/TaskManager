using Generator.Attributes;
using TaskManager.Domain.Abstractions;
using TaskManager.Domain.Exceptions;

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
    
    public string? Description { get; init; }
    
    public DateTime CreationDate { get; init; }
    
    public DateTime? ClosingDate { get; set; }

    public Status Status => _status;

    public DateTime EstimatedCompletionDate { get; init; }

    public long? ActualProcessingTime => ClosingDate is null ? null : ClosingDate.Value.Ticks - CreationDate.Ticks;
    
    public void StartProcessing()
    {
        if (_status != Status.Created)
        {
            throw new DomainException("Задача уже начата или завершена");
        }
        
        _status = Status.Processing;
    }

    public void Complete()
    {
        if (_status == Status.Completed)
        {
            throw new DomainException("Задача уже завершена");
        }
        
        _status = Status.Completed;
        ClosingDate = DateTime.UtcNow;
    }
}

public enum Status
{
    Created = 0,
    Processing = 1,
    Completed = 2
}