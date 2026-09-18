using System.ComponentModel.DataAnnotations;

namespace api.Dtos;

public class UpdateIssueDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty;

    public int? CategoryId { get; set; }
}