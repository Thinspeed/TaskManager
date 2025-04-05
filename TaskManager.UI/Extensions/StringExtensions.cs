using Microsoft.AspNetCore.Http.Extensions;

namespace TaskManager.UI.Extensions;

public static class StringExtensions
{
    private const string FiltersParameterName = "Filters";
    private const string SortsParameterName = "Sorts";
    private const string PageParameterName = "Page";
    private const string PageSizeParameterName = "PageSize";
    
    public static string CreatePagedUri(this string basePath, int page, int pageSize)
    {
        var queryParams = new Dictionary<string, string>
        {
            { PageParameterName, page.ToString() },
            { PageSizeParameterName, pageSize.ToString() }
        };
        
        var queryBuilder = new QueryBuilder(queryParams);
        
        return basePath + queryBuilder;
    }

    public static string CreateSieveUri(this string basePath, string sorts, string filters, int page, int pageSize)
    {
        var queryParams = new Dictionary<string, string>
        {
            {FiltersParameterName, filters },
            { SortsParameterName, sorts },
            { PageParameterName, page.ToString() },
            { PageSizeParameterName, pageSize.ToString() }
        };
        
        var queryBuilder = new QueryBuilder(queryParams);
        
        return basePath + queryBuilder;
    }
}