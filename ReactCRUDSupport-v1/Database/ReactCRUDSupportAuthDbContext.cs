using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ReactCRUDSupport_v1.Database
{
    public class ReactCRUDSupportAuthDbContext : IdentityDbContext
    {
        public ReactCRUDSupportAuthDbContext(DbContextOptions<ReactCRUDSupportAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var reader = "d0a10379-35d4-456e-8a24-ae0130e4f27f";
            var writer = "9fb936d7-145e-4ed3-96a7-a4b67059d16d";

            var roles = new List<IdentityRole> { 
                new IdentityRole{ 
                    Id=reader,
                    ConcurrencyStamp=reader,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole{ 
                    Id=writer,
                    ConcurrencyStamp=writer,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
