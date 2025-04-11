using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Wallet.Server.DAL;

public class ContextFactory : IDesignTimeDbContextFactory<Context>
{
    public Context CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            
#if DEBUG
        var connectionString = config.GetConnectionString("ProductionConnection");
#elif RELEASE
        var connectionString = config.GetConnectionString("ProductionConnection");
#endif

        return new Context(connectionString);
    }

    private string[] args = [];

    public Context CreateDbContext()
    {
        return CreateDbContext(args);
    }
}