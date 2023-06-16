namespace Data.Configurations;

internal class EmoteConfiguration : IEntityTypeConfiguration<Emote>
{
    public void Configure(EntityTypeBuilder<Emote> entity)
    {
        entity.HasKey(e => e.ClassName);
    }
}