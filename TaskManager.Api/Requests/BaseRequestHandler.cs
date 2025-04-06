using System.Linq.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace TaskManager.Api.Requests;

public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

public abstract class BaseRequestHandler<TRequest, TBaseType, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse> 
    where TResponse : PagedList<TBaseType>
{
    private const int DefaultPage = 1;
    private const int DefaultPageSize = 20;
    
    protected readonly DbContext DbContext;
    
    private readonly SieveProcessor _sieveProcessor;

    public BaseRequestHandler(DbContext dbContext, SieveProcessor sieveProcessor)
    {
        DbContext = dbContext;
        _sieveProcessor = sieveProcessor;
    }
    
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    protected async Task<PagedList<TBaseType>> CreatePagedListAsync<T>(
        IQueryable<T> query, 
        SieveModel sieveModel,
        Expression<Func<T, TBaseType>> selector,
        CancellationToken cancellationToken)
    {
        ValidateAndSetSieveModel(sieveModel);
        
        int totalCount = await query.CountAsync(cancellationToken);
        
        IQueryable<T> sieveQuery = _sieveProcessor!.Apply(sieveModel, query);
        IQueryable<TBaseType> resultQuery = sieveQuery.Select(selector);

        return new PagedList<TBaseType>()
        {
            TotalCount = totalCount,
            Page = sieveModel.Page!.Value,
            PageSize = sieveModel.PageSize!.Value,
            TotalPages = (int)Math.Ceiling((double)totalCount / sieveModel.PageSize!.Value),
            Data = await resultQuery.ToListAsync(cancellationToken)
        };
    }
    
    private void ValidateAndSetSieveModel(SieveModel sieveModel)
    {
        sieveModel.PageSize ??= DefaultPageSize;
        sieveModel.Page ??= DefaultPage;
    }
}