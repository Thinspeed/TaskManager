using System.ComponentModel.DataAnnotations;

namespace TaskManager.UI.Infrastructure.Shared;

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is DateTime dateTime)
        {
            return dateTime > DateTime.Now;
        }

        return false;
    }
}