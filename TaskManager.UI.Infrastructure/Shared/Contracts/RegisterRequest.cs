namespace TaskManager.UI.Infrastructure.Shared.Contracts;

public class RegisterRequest
{
    public string Login { get; set; }
    
    public string Password { get; set; }
    
    public string Name { get; set; }
}