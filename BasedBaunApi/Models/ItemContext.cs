using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BasedBaunApi.Models;

using Microsoft.EntityFrameworkCore;

public class ItemContext : IdentityUserContext<IdentityUser> 
{
    public ItemContext(DbContextOptions<ItemContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("now() at time zone 'utc'");
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Item> Items { get; set; } = null!;
}