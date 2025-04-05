using Microsoft.AspNetCore.Http.Extensions;

namespace TaskManager.UI.Extensions;

public static class StringExtensions
{
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
}