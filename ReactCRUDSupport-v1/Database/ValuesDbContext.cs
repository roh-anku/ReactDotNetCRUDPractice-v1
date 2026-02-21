using Microsoft.EntityFrameworkCore;
using ReactCRUDSupport_v1.Models.Domain;

namespace ReactCRUDSupport_v1.Database
{
    public class ValuesDbContext : DbContext
    {
        public ValuesDbContext(DbContextOptions<ValuesDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            AddDomain add = new AddDomain { Id = Guid.Parse("eaef8e29-64f4-4536-a3a8-943d8b8eab0d"), Value = 0 };

            modelBuilder.Entity<AddDomain>().HasData(add);
        }

        public DbSet<AddDomain> AddOne { get; set; }
    }
}
