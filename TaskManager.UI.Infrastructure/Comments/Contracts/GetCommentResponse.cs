using TaskManager.UI.Infrastructure.Shared.Contracts;

namespace TaskManager.UI.Infrastructure.Comments.Contracts;

public class GetCommentResponse
{
    public int CommentId { get; set; }
    
    public string Content { get; set; }
    
    public DateTime CreationDate { get; set; }

    public GetUserResponse User { get; set; }
}