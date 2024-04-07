using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myEShop.Web.Models;

namespace myEShop.Web;

public class ApplicationDbContext
    : IdentityDbContext<User, IdentityRole<int>, int>
{

    private string hashedPassword = string.Empty;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Name = "Admin", Id = "1" },
            new IdentityRole { Name = "User", Id = "2" }
        );

        var PH = new PasswordHasher<User>();
        string hp = PH.HashPassword(null, "1qaz!QAZ");
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, UserName = "admin@gmail.com", NormalizedUserName = "ADMIN@GMAIL.COM", Email = "admin@gmail.com", NormalizedEmail = "ADMIN@GMAIL.COM", EmailConfirmed = true, SecurityStamp = string.Empty, PasswordHash = hp }
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { RoleId = "1", UserId = "1" }
        );
    }
}
