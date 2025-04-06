namespace TaskManager.UI.Infrastructure.Comments.Contracts;

public class CreateCommentRequest
{
    public int CardId { get; set; }
    
    public required string Content { get; set; }
}