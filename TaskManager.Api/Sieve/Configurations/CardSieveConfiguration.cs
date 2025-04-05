using JetBrains.Annotations;
using Sieve.Services;
using TaskManager.Domain;

namespace TaskManager.Api.Sieve.Configurations;

[UsedImplicitly]
public class CardSieveConfiguration : ISieveConfiguration
{
    public void Configure(SievePropertyMapper mapper)
    {
        mapper.Property<Card>(x => x.Id)
            .CanSort();
        
        mapper.Property<Card>(x => x.CreationDate)
            .CanSort()
            .CanFilter();

        mapper.Property<Card>(x => x.ClosingDate)
            .CanSort()
            .CanFilter();
        
        mapper.Property<Card>(x => x.User.Id)
            .CanSort()
            .CanFilter();
        
        mapper.Property<Card>(x => x.Status)
            .CanSort()
            .CanFilter();
    }
}