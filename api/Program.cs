using api.Data;
using api.Services;
using api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IIssueService, IssueService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<IssueDbContext>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<IssueDbContext>();

    if (!context.Issues.Any())
    {
        context.Issues.AddRange(
            new Issue
            {
                Title = "Inloggningen fungerar inte",
                Description = "Användaren får ett felmeddelande.",
                Status = "Open",
                CreatedAt = DateTime.UtcNow
            },
            new Issue
            {
                Title = "Lägg till export till Excel",
                Description = null,
                Status = "In Progress",
                CreatedAt = DateTime.UtcNow
            },
            new Issue
            {
                Title = "Uppdatera kundregistret",
                Description = "Lägg till fält för organisationsnummer.",
                Status = "Done",
                CreatedAt = DateTime.UtcNow
            }
        );

        context.SaveChanges();
    }

    if (!context.Categories.Any())
    {
        context.Categories.AddRange(
            new Category
            {
                Name = "Bug"
            },
            new Category
            {
                Name = "Feature"
            },
            new Category
            {
                Name = "Support"
            }
        );

        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
