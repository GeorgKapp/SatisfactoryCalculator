namespace Data.Context;

//Used to overwrite the existing data with new one
public class TempModelContext : ModelContext
{
    public TempModelContext() { }

    public TempModelContext(DbContextOptions<TempModelContext> options) : base(options) { }
}
