using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using STA.Core.Helper;
using STA.Stock.Data;

namespace STA.User.Data
{
    public class UserDbContextFactory : IDesignTimeDbContextFactory<StockDbContext>
    {
        public StockDbContext CreateDbContext(string[] args)
        {
            IConfiguration config = ConfigrationHelper.Get("../../Service/STA.StockAPI");

            var optionsBuilder = new DbContextOptionsBuilder<StockDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new StockDbContext(optionsBuilder.Options);
        }
    }
}
