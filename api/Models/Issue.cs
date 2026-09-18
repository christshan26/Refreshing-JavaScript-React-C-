namespace api.Models;

public class Issue
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
}