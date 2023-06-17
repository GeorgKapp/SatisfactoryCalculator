namespace Data.Configurations;

internal class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> entity)
    {
        entity.ToTable(nameof(Item));
        
        entity.HasKey(p => p.ClassName);
        
        entity.Property(p => p.Form)
            .HasConversion<string?>();
        
        entity.Property(p => p.StackSize)
            .HasConversion<string>();
    }
}