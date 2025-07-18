

namespace Agriculture.DAL.Models;

public class EbookContextFactory : IDesignTimeDbContextFactory<AgriContext>
{
    private string cs =
        "Server=localhost;Database=AgriDB;User Id=sa;Password=Abdo@1234;TrustServerCertificate=True";
    public AgriContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AgriContext>();
        optionsBuilder.UseSqlServer(cs);

        return new AgriContext(optionsBuilder.Options);
    }
}