using api.Dtos;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IssuesController : ControllerBase
{
    private readonly IIssueService _issueService;

    public IssuesController(IIssueService issueService)
    {
        _issueService = issueService;
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _issueService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<List<IssueDto>>> GetAll()
    {
        var issues = await _issueService.GetAllAsync();
        
        return Ok(issues);
    }

    [HttpPost]
    public async Task<ActionResult<IssueDto>> Create(
        CreateIssueDto createIssueDto)
    {
        var createdIssue = await _issueService.CreateAsync(createIssueDto);

        return Created(
            $"/api/issues/{createdIssue.Id}",
            createdIssue
        );
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<IssueDto>> Update(
        int id,
        UpdateIssueDto updateIssueDto)
    {
        var updatedIssue = await _issueService.UpdateAsync(
            id,
            updateIssueDto
            );

        if (updatedIssue is null)
        {
            return NotFound();
        }

        return Ok(updatedIssue);
    }
}
