using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class IssueDbContext : DbContext
{
    public IssueDbContext(DbContextOptions<IssueDbContext> options)
     : base(options)
    {
    }

    public DbSet<Issue> Issues { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Issue>()
            .HasOne(issue => issue.Category)
            .WithMany(category => category.Issues)
            .HasForeignKey(issue => issue.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}