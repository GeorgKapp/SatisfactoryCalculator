namespace Data.Configurations;

internal class SchematicDependencyConfiguration : IEntityTypeConfiguration<SchematicDependency>
{
    public void Configure(EntityTypeBuilder<SchematicDependency> entity)
    {
        entity.Property(e => e.ID)
            .ValueGeneratedOnAdd();
        
        entity.Property(d => d.SchematicDependencyType)
            .HasConversion<string>();
    }
}
