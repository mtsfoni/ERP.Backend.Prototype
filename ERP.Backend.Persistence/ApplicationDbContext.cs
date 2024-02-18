using Bogus;
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

    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : DbContext(options)
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Price> Prices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
            modelBuilder.Entity<Article>()
                .Navigation(article => article.Prices) 
                .AutoInclude();            
        }

        public async Task LoadDemoData()
        {           

            // already seeded
            if (Articles.Any())
                return;

            // sample data will be different due
            // to the nature of generating data
            var fake = new Faker<Article>()
                .Rules((f, v) => v.Name = f.Commerce.ProductName())
                .Rules((f, v) => v.Brand = f.Company.CompanyName());
            var articles = fake.Generate(100);

            Articles.AddRange(articles);
            await SaveChangesAsync();
        }
    }
}
