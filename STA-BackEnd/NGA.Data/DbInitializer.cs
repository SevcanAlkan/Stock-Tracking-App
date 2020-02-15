using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NGA.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NGA.Data
{
    //https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding
    //Custom data initialization method can try, when this method stuck to a limitation.
    public static class DbInitializer
    {
        public static void Initialize(NGADbContext context)
        {
            //  modelBuilder.Entity<Parameter>().OwnsOne(p => p.Code).HasData(
            //     new { Id = new Guid(), CreateBy = Guid.Empty, CreateDT = DateTime.Now, IsDeleted = false, Name = "LogSystem", Code = "SYS01001", GroupCode = "SYS", OrderIndex = 1, Value = "1"  });

            // modelBuilder.Entity<Group>().OwnsOne(p => p.Name).HasData(
            //     new { Id = new Guid(), CreateBy = Guid.Empty, CreateDT = DateTime.Now, IsDeleted = false, Name = "Main", Description = "The main chat group of application", IsMain = true, IsPrivate = false  });

            context.Database.EnsureCreated();

            if (!context.Parameters.Any(a => a.Code == "SYS01001")) { context.Parameters.Add(new Parameter() { Id = new Guid(), CreateBy = Guid.Empty, CreateDT = DateTime.Now, IsDeleted = false, Name = "LogSystem", Code = "SYS01001", GroupCode = "SYS", OrderIndex = 1, Value = "1" }); }

            if (!context.Groups.Any(a => a.Name == "Main")) { context.Groups.Add(new Group() { Id = new Guid(), CreateBy = Guid.Empty, CreateDT = DateTime.Now, IsDeleted = false, Name = "Main", Description = "The main chat group of application", IsMain = true, IsPrivate = false }); }

            context.SaveChanges();
        }
    }
}
