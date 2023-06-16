namespace Data.Configurations;

internal class SchematicCostConfiguration : IEntityTypeConfiguration<SchematicCost>
{
    public void Configure(EntityTypeBuilder<SchematicCost> entity)
    {
        entity.Property(e => e.ID)
            .ValueGeneratedOnAdd();
    }
}