using EntityFramework.Persistence.Configurations.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain;

namespace EntityFramework.Persistence.Configurations;

public class CardConfiguration : EntityConfiguration<Card>
{
    public override void Configure(EntityTypeBuilder<Card> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Status).HasConversion<int>();
        
        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
    }
}