using System.ComponentModel.DataAnnotations;

namespace TaskManager.UI.Services.Validation;

public class NotWhiteSpaceAttribute : ValidationAttribute
{
    public NotWhiteSpaceAttribute()
    {
        ErrorMessage = "Поле не может быть пустым или состоять только из пробелов.";
    }

    public override bool IsValid(object? value)
    {
        if (value is string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        return false;
    }
}