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

    [HttpGet]
    public async Task<ActionResult<List<IssueDto>>> GetAll()
    {
        var issues = await _issueService.GetAllAsync();
        
        return Ok(issues);
    }
}