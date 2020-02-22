using System;
using System.Collections.Generic;
using System.Text;
using STA.Stock.Model.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace STA.Stock.Data
{
    public class StockDbContext : DbContext
    {
        public StockDbContext()
        {
        }

        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options)
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

        public DbSet<AdCampainDTO> adCampains { get; set; }
        public DbSet<BrandDTO> brands { get; set; }
        public DbSet<ColorDTO> colors { get; set; }
        public DbSet<CustomerDTO> customers { get; set; }
        public DbSet<InfuluencerDTO> infuluencers { get; set; }
        public DbSet<ModelDTO> models { get; set; }
        public DbSet<OrderDTO> orders { get; set; }
        public DbSet<PictureDTO> pictures { get; set; }
        public DbSet<ProductDTO> products { get; set; }
        public DbSet<ProductStockDTO> productStocks { get; set; }
    }
}
