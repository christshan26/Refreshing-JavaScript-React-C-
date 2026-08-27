using api.Dtos;
using api.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class IssueService : IIssueService
{
    private readonly IssueDbContext _context;

    public IssueService(IssueDbContext context)
    {
        _context = context;
    }

    public async Task<List<IssueDto>> GetAllAsync()
    {
        return await _context.Issues
            .AsNoTracking()
            .OrderByDescending(issue => issue.CreatedAt)
            .Select(issue => new IssueDto
            {
                Id = issue.Id,
                Title = issue.Title,
                Description = issue.Description,
                Status = issue.Status,
                CreatedAt = issue.CreatedAt
            })
            .ToListAsync();
    }
}