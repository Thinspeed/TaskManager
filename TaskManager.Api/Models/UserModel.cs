using EFSelector;
using TaskManager.Domain;

namespace TaskManager.Api.Models;

public class UserModel
{
    public int Id { get; set; }
        
    public string Name { get; set; }

    public static readonly EfSelector<User, UserModel> Selector =
        EfSelector.Declare<User, UserModel>()
            .Select(src => src.Id, dst => dst.Id)
            .Select(src => src.Name, src => src.Name);
}