using System;
using System.Collections.Generic;
using System.Text;

namespace STA.User.Data
{
    public static class DbInitializer
    {
        public static void Initialize(UserDbContext context)
        {
            context.Database.EnsureCreated();

            //if (!context.Parameters.Any(a => a.Code == "SYS01001")) { context.Parameters.Add(new Parameter() { Id = new Guid(), CreateBy = Guid.Empty, CreateDT = DateTime.Now, IsDeleted = false, Name = "LogSystem", Code = "SYS01001", GroupCode = "SYS", OrderIndex = 1, Value = "1" }); }

            context.SaveChanges();
        }
    }
}
