using api.Dtos;

namespace api.Services;

public interface IIssueService
{
    Task<List<IssueDto>> GetAllAsync();

    Task<IssueDto> CreateAsync(CreateIssueDto createIssueDto);

    Task<IssueDto?> UpdateAsync(
        int id,
        UpdateIssueDto updateIssueDto
    );
}