namespace Data.Configurations;

internal class MinerConfiguration : IEntityTypeConfiguration<Miner>
{
    public void Configure(EntityTypeBuilder<Miner> entity)
    {
        entity.ToTable(nameof(Miner));
        
        entity.HasKey(p => p.ClassName);
        
        entity.Property(d => d.AllowedResourceForm)
            .HasConversion<string>();
        
        entity.HasOne(p => p.Building)
            .WithOne(p => p.Miner)
            .HasForeignKey<Miner>(p => p.ClassName);

        entity.HasMany(p => p.ExtractableResources)
            .WithMany(p => p.Miners)
            .UsingEntity(p => p.ToTable("MinerResource"));
    }
}