namespace TaskManager.UI.Sieve;

public class FilterTerm
{
    public required string Field { get; set; }
        
    public required object? Value { get; set; }
        
    public required FilterOperation Operation { get; set; }

    public override string ToString()
    {
        return $"{Field}{GetStringFilterOperation(Operation)}{Value}";
    }

    public string GetKey()
    {
        return $"{Field}{Operation}";
    }
        
    private string GetStringFilterOperation(FilterOperation operation) => operation switch
    {
        FilterOperation.Equal => "==",
        FilterOperation.LessOrEqual => "<=",
        FilterOperation.MoreOrEqual => ">=",
        FilterOperation.Less => "<",
        FilterOperation.More => ">",
        FilterOperation.Contains => "@=",
        _ => ""
    };
}