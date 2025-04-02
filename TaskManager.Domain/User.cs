using Generator.Attributes;
using TaskManager.Domain.Abstractions;

namespace TaskManager.Domain;

[EfConstructor]
public partial class User : Entity
{
    public User(string login, string passwordHash, string name)
    {
        Login = login;
        PasswordHash = passwordHash;
        Name = name;
    }
    
    public string Login { get; init; }
    
    public string PasswordHash { get; init; }
    
    public string Name { get; set; }
}