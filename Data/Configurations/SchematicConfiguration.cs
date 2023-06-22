namespace Data.Configurations;

internal class SchematicConfiguration : IEntityTypeConfiguration<Schematic>
{
    public void Configure(EntityTypeBuilder<Schematic> entity)
    {
        entity.ToTable(nameof(Schematic));
        
        entity.HasKey(p => p.ClassName);
        
        entity.Property(p => p.SchematicType)
            .HasConversion<string>();

        entity
            .Property(p => p.RelevantEvent)
            .HasConversion<string?>();
        
        entity
            .HasMany(p => p.UnlocksRecipes)
            .WithMany()
            .UsingEntity(p => p.ToTable("SchematicRecipeUnlock"));
        
        entity
            .HasMany(p => p.UnlocksEmotes)
            .WithMany()
            .UsingEntity(p => p.ToTable("SchematicEmoteUnlock"));
        
        entity
            .HasMany(p => p.UnlocksScannerResources)
            .WithMany()
            .UsingEntity(p => p.ToTable("SchematicScannerResourceUnlock"));
        
        entity
            .HasMany(p => p.UnlocksScannerResourcePairs)
            .WithMany()
            .UsingEntity(p => p.ToTable("SchematicScannerResourcePairUnlock"));
        
        entity
            .HasMany(p => p.GivesItems)
            .WithMany()
            .UsingEntity(p => p.ToTable("SchematicItemGive"));
        
        entity
            .HasMany(p => p.UnlocksScannableObjects)
            .WithOne();
        
        entity
            .HasMany(p => p.Costs)
            .WithOne()
            .IsRequired();
        
        entity
            .HasMany(p => p.Dependencies)
            .WithOne();
    }
}
