using api.Dtos;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Models;

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

    public async Task<IssueDto> CreateAsync(CreateIssueDto createIssueDto)
    {
        var issue = new Issue
        {
            Title = createIssueDto.Title,
            Description = createIssueDto.Description,
            Status = createIssueDto.Status,
            CreatedAt = DateTime.UtcNow
        };

        await _context.Issues.AddAsync(issue);
        
        await _context.SaveChangesAsync();

        return new IssueDto
        {
            Id = issue.Id,
            Title = issue.Title,
            Description = issue.Description,
            Status = issue.Status,
            CreatedAt = issue.CreatedAt
        };
    }
}