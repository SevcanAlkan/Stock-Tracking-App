using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using STA.User.Model.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace STA.User.Data
{
    public class UserDbContext : IdentityDbContext<UserDTO, RoleDTO, Guid>
    {
        public UserDbContext()
        {
        }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException("modelBuilder");

            //modelBuilder.ApplyConfiguration(new GroupMap());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //DbInitializer.Initialize(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public virtual void Commit()
        {
            try
            {
                base.SaveChanges();
            }
            catch (Exception dbEx)
            {
                throw dbEx;
            }
        }

    }
}
