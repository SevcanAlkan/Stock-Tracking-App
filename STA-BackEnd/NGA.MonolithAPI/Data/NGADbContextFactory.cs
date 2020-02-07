using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NGA.Data;
using NGA.MonolithAPI.Helper;
using System.IO;

namespace NGA.MonolithAPI.Data
{
    public class NGADbContextFactory : IDesignTimeDbContextFactory<NGADbContext>
    {
        public NGADbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<NGADbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection").Replace("{HostMachineIpAddress}", GetHostMachineIP.Get());
            builder.UseSqlServer(connectionString);
            return new NGADbContext(builder.Options);
        }
    }
}
