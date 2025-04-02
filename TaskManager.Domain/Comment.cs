using Generator.Attributes;
using TaskManager.Domain.Abstractions;

namespace TaskManager.Domain;

[EfConstructor]
public partial class Comment : Entity
{
    [RelationId(RelationType = typeof(User))]
    private int _userId;

    [RelationId(RelationType = typeof(Card))]
    private int _cardId;

    public Comment(int userId, int cardId, string content)
    {
        UserId = userId;
        CardId = cardId;
        Content = content;
        
        CreationDate = DateTime.UtcNow;
    }
    
    public string Content { get; init; }
    
    public DateTime CreationDate { get; init; }
}