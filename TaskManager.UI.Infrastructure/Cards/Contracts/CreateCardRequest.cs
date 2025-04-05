using System.ComponentModel.DataAnnotations;
using TaskManager.UI.Infrastructure.Shared;

namespace TaskManager.UI.Infrastructure.Cards.Contracts;

public class CreateCardRequest
{
    [Required(ErrorMessage = "Название обязательно")]
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Дата обязательна")]
    [FutureDate(ErrorMessage = "Дата должна быть в будущем")]
    public DateTime EstimatedCompletionDate { get; set; }
}