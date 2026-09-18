using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IssueDbContext _context;

        public CategoriesController(IssueDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .OrderBy(category => category.Name)
                .Select(category => new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                })
                .ToListAsync();

            return Ok(categories);
        }
    }
}
