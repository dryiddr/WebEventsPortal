using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Persistence.EntityConfigurations;

public class RecommendationConfiguration : IEntityTypeConfiguration<Recommendation>
{
    public void Configure(EntityTypeBuilder<Recommendation> builder)
    {
        builder.ToTable("recommendations");
        builder.HasKey(recommendation => recommendation.Id);

        builder.HasOne(recommendation => recommendation.User)
            .WithOne(user => user.Recommendation);
        
        builder.Property(recommendation => recommendation.FoundWords)
            .HasConversion(v => string.Join(" ", v.ToArray()),
                v => v.Split(' ', StringSplitOptions.None).ToList());
    }
}