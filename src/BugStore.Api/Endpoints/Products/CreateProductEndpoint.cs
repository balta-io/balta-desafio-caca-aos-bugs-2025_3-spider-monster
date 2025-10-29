using BugStore.Handlers.Products;

namespace BugStore.Api.Endpoints.Products;

public static class CreateProductEndpoint
{
    public static void MapCreateProductEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (ProductHandler handler, Requests.Products.Create request) =>
        {
            var result = await handler.Handle(request);
            return Results.Created($"/products/{result.Id}", result);
        })
        .WithName("CreateProduct")
        .WithTags("Products");
    }
}
