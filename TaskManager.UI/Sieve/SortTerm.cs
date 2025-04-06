namespace TaskManager.UI.Sieve;

public struct SortTerm
{
    public required string Field { get; init; }
    
    public required SortDirection Direction { get; init; }

    public override string ToString()
    {
        if (Direction == SortDirection.None)
        {
            return string.Empty;
        }

        return Direction == SortDirection.Asc ? Field : $"-{Field}";
    }
}