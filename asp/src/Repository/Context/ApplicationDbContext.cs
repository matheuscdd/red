using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Repository.Context;

public class ApplicationDbContext(DbContextOptions dbContextOptions) : IdentityDbContext<User>(dbContextOptions)
{
    public DbSet<Link> Stocks { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Access> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                },
                new IdentityRole
                {
                    Name = "Common",
                    NormalizedName = "COMMON",
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
}