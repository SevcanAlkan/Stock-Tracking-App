using System.IO;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NGA.Core;
using NGA.Data.Helper;

namespace NGA.Data
{
    public class NGADbContextFactory : IDesignTimeDbContextFactory<NGADbContext>
    {
        public NGADbContext CreateDbContext(string[] args)
        {
            IConfiguration config = ConfigrationHelper.Get();

            StaticValues.HostAddress = (IPAddress.Loopback.ToString() + ":" + config.GetValue<int>("Host:Port")).ToString();
            StaticValues.HostSSLAddress = (IPAddress.Loopback.ToString() + ":" + config.GetValue<int>("Host:PortSSL")).ToString();

            StaticValues.DBConnectionString = config.GetConnectionString("DefaultConnection").Replace("{HostMachineIpAddress}", GetHostMachineIP.Get());

            var optionsBuilder = new DbContextOptionsBuilder<NGADbContext>();
            optionsBuilder.UseSqlServer(StaticValues.DBConnectionString);

            return new NGADbContext(optionsBuilder.Options);
        }
    }
}