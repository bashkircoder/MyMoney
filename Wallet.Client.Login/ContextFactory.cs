using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Wallet.Client.Login;

public class ContextFactory : IDesignTimeDbContextFactory<Context>
{
    private static string? _connectionString;
    public Context CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        
        _connectionString = config.GetConnectionString("ProductionConnection");


        return new Context(_connectionString);
    }
    
    private string[] args = [];

    public Context CreateDbContext()
    {
        return CreateDbContext(args);
    }
    
}