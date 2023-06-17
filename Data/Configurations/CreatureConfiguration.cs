using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.Configurations;

internal class CreatureConfiguration : IEntityTypeConfiguration<Creature>
{
    public void Configure(EntityTypeBuilder<Creature> entity)
    {
        entity.ToTable(nameof(Creature));
        
        entity.HasKey(p => p.ClassName);
        
        entity.Property(p => p.Behaviour)
            .HasConversion<string>();
        
        entity.Property(p => p.Damage)
            .HasConversion(
                p => string.Join(",", p),
                p => string.IsNullOrEmpty(p) ? new List<string>() : p.Split(',', StringSplitOptions.RemoveEmptyEntries),
                new ValueComparer<ICollection<string>>(
                (c1, c2) => c1!.SequenceEqual(c2!),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c)
                );
        
        entity
            .HasMany(p => p.Variants)
            .WithMany()
            .UsingEntity(p => p.ToTable("CreatureVariant"));
    }
}