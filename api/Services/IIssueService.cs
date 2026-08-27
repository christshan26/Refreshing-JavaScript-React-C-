using api.Dtos;

namespace api.Services;

public interface IIssueService
{
    Task<List<IssueDto>> GetAllAsync();
}