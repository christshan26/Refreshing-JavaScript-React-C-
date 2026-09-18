using System;

namespace api.Models;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<Issue> Issues { get; set; } = [];
}
