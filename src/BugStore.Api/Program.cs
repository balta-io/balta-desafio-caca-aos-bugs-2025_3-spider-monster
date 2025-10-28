using BugStore.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(
x=>
{
    x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
    

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/v1/products", () => "Hello World!");
app.MapGet("/v1", () => "Hello World!");
app.MapPost("/v1", () => "Hello World!");
app.MapDelete("/v1", () => "Hello World!");



app.Run();