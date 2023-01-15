using FastFood.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FastFood.Models;

namespace FastFood.Data;

public class FastFoodContext : IdentityDbContext<FastFoodUser>
{
    public FastFoodContext(DbContextOptions<FastFoodContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        RenameIdentityTables(builder);

        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    protected void RenameIdentityTables(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("FFUser");
        builder.Entity<FastFoodUser>(entity =>
        {
            entity.ToTable(name: "Users");
        });
        builder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable(name: "Roles");
        });
        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoles");
        });
        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("UserClaims");
        });
        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("UserLogins");
        });
        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("RoleClaims");
        });
        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserTokens");
        });
    }
    public DbSet<FastFood.Models.Meal> Meal { get; set; }
    public DbSet<FastFood.Models.Order> Order { get; set; }
    public DbSet<FastFood.Models.Plate> Plate { get; set; }
    public DbSet<FastFood.Models.Favorite> Favorite { get; set; }
}
