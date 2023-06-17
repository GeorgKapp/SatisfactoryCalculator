namespace Data.Configurations;

internal class GeneratorConfiguration : IEntityTypeConfiguration<Generator>
{
    public void Configure(EntityTypeBuilder<Generator> entity)
    {
        entity.ToTable(nameof(Generator));
        
        entity.HasKey(p => p.ClassName);
        
        entity.HasOne(p => p.Building)
            .WithOne(p => p.Generator)
            .HasForeignKey<Generator>(p => p.ClassName);
        
        entity
            .HasMany(p => p.Fuels)
            .WithOne()
            .IsRequired();
    }
}