using Sieve.Services;
using TaskManager.Domain;

namespace TaskManager.Api.Sieve.Configurations;

public class CommentSieveConfiguration : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<Comment>(x => x.Card.Id)
            .CanFilter();
    }
}