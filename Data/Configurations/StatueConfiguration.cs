namespace Data.Configurations;


internal class StatueConfiguration : IEntityTypeConfiguration<Statue>
{
    public void Configure(EntityTypeBuilder<Statue> entity)
    {
        entity.ToTable(nameof(Statue));
        
        entity.HasKey(e => e.ClassName);
    }
}
