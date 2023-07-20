using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BasedBaunApi.Models;

public class ItemContext : IdentityUserContext<IdentityUser>
{
    public ItemContext(DbContextOptions<ItemContext> options)
        : base(options)
    {
    }

    public DbSet<Item> Items { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("now() at time zone 'utc'");
        base.OnModelCreating(modelBuilder);
    }
}
