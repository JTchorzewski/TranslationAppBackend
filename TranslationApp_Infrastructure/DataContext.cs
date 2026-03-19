using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using TranslationApp_Domain.Model;

namespace TranslationApp_Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<RequestLog> TranslationLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RequestLog>()
                .HasIndex(x => x.Translator);

            modelBuilder.Entity<RequestLog>()
                .HasIndex(x => x.CreatedAtUtc);
        }
    }
}
