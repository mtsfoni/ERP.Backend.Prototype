using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Backend.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Price> Prices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=../ERP-Database.db");
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
