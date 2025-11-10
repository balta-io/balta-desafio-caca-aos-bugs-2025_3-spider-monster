using BugStore.Data;
using BugStore.Handlers.Customers;
using BugStore.Api.Endpoints.Customer;
using BugStore.Api.Endpoints.Products;
using Microsoft.EntityFrameworkCore;
using BugStore.Handlers.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Ensure unique schema ids for types with same name in different namespaces
    options.CustomSchemaIds(type => type.FullName?.Replace("+", ".") ?? type.Name);

    // If there are conflicting API descriptions (rare in minimal APIs), pick the first
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<CustomerHandler>();
builder.Services.AddScoped<ProductHandler>();

var app = builder.Build();

// Global exception logging middleware to capture exceptions from Swagger generation and other middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Unhandled exception in request pipeline:");
        Console.WriteLine(ex.ToString());
        throw;
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CUSTOMER 
app.MapCreateCustomerEndpoint();
app.MapGetCustomersEndpoint();
app.MapGetCustomerByIdEndpoint();
app.MapUpdateCustomerEndpoint();
app.MapDeleteCustomerEndpoint();

// PRODUCT 
app.MapCreateProductEndpoint();
app.MapGetProductsEndpoint();
app.MapGetProductByIdEndpoint();
app.MapUpdateProductEndpoint();
app.MapDeleteProductEndpoint();

app.Run();
