using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using JAKList.Models;

namespace JAKList.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // any guid
        const string ADMIN_ID = "07307a26-d20f-493e-b42a-c75636c141c9";
        // any guid, but nothing is against to use the same one
        const string ROLE_ID = "68667585-c95d-4767-bab0-b7aae4b65502";
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = ROLE_ID,
            Name = "Administrator",
            NormalizedName = "ADMINISTRATOR"
        });

        // var hasher = new PasswordHasher<ApplicationUser>();
        // builder.Entity<ApplicationUser>().HasData(new ApplicationUser
        // {
        //     Id = ADMIN_ID,
        //     UserName = "admin",
        //     NormalizedUserName = "ADMIN",
        //     Email = "admin@jaklist.com",
        //     NormalizedEmail = "ADMIN@JAKLIST.COM",
        //     EmailConfirmed = true,
        //     // LockoutEnabled = true,
        //     PhoneNumberConfirmed = true,
        //     PasswordHash = hasher.HashPassword(null, "AdminPass70-1"),
        //     SecurityStamp = Guid.NewGuid().ToString()
        // });

        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = ROLE_ID,
            UserId = ADMIN_ID
        });
    }
    public DbSet<TodoItem> Items { get; set; }
}