using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Backend.Models
{
    public enum DatabaseType
    { 
        SQLite,
        PostgreSQL
    }

    public class ApplicationDbContext
        (DbContextOptions<ApplicationDbContext> options) 
        : DbContext(options)
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Price> Prices { get; set; }

        public static void Configure(DbContextOptionsBuilder optionsBuilder, DatabaseType databaseType, string? connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("No valid connection string provided for the database.");
            }

            if (!optionsBuilder.IsConfigured)
            {
                if (databaseType == DatabaseType.PostgreSQL)
                {
                    optionsBuilder.UseNpgsql(connectionString);
                }
                else 
                {   
                    optionsBuilder.UseSqlite(connectionString);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
            modelBuilder.Entity<Article>()
                .Navigation(article => article.Prices) 
                .AutoInclude();
        }
    }
}
