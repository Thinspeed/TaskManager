using EntityFramework.Persistence.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly,
                type => type is { IsClass: true, IsAbstract: false } && type.IsSubclassOf(typeof(EntityConfiguration<>)));
    }
}