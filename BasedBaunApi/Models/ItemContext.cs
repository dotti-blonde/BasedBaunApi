namespace BasedBaunApi.Models;

using Microsoft.EntityFrameworkCore;

public class ItemContext : DbContext
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
    }

    public DbSet<Item> Items { get; set; } = null!;
}