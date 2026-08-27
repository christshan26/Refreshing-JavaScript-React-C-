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
}