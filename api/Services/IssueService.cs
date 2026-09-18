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

    public async Task<bool> DeleteAsync(int id)
    {
        var issue = await _context.Issues
            .FirstOrDefaultAsync(issue => issue.Id == id);

        if (issue is null)
        {
            return false;
        }

        _context.Issues.Remove(issue);
        await _context.SaveChangesAsync();

        return true;
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
                CreatedAt = issue.CreatedAt,
                CategoryId = issue.CategoryId,
                CategoryName = issue.Category != null ? issue.Category.Name : null
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
            CreatedAt = DateTime.UtcNow,
            CategoryId = createIssueDto.CategoryId
        };

        await _context.Issues.AddAsync(issue);
        
        await _context.SaveChangesAsync();

        string? categoryName = null;
        if (issue.CategoryId is not null)
        {
            categoryName = await _context.Categories
                .Where(category => category.Id == issue.CategoryId)
                .Select(category => category.Name)
                .FirstOrDefaultAsync();
        }

        return new IssueDto
        {
            Id = issue.Id,
            Title = issue.Title,
            Description = issue.Description,
            Status = issue.Status,
            CategoryId = issue.CategoryId,
            CategoryName = categoryName,
            CreatedAt = issue.CreatedAt
        };
    }

    public async Task<IssueDto?> UpdateAsync(
        int id,
        UpdateIssueDto updateIssueDto)

    {
        var issue = await _context.Issues
            .FirstOrDefaultAsync(issue => issue.Id == id);

            if (issue == null)
            {
                return null;
            }

            issue.Title = updateIssueDto.Title;
            issue.Description = updateIssueDto.Description;
            issue.Status = updateIssueDto.Status;
            issue.CategoryId = updateIssueDto.CategoryId;

            await _context.SaveChangesAsync();

            return await _context.Issues
                .AsNoTracking()
                .Where(issue => issue.Id == id)
                .Select(issue => new IssueDto
            {
                Id = issue.Id,
                Title = issue.Title,
                Description = issue.Description,
                Status = issue.Status,
                CategoryId = issue.CategoryId,
                CategoryName = issue.Category != null ? issue.Category.Name : null,
                CreatedAt = issue.CreatedAt
            })
                .FirstAsync();
    }
}
