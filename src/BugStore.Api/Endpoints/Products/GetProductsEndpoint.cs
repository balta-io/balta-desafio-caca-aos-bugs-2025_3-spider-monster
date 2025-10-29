using BugStore.Handlers.Products;

namespace BugStore.Api.Endpoints.Products;

public static class GetProductsEndpoint
{
    public static void MapGetProductsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ProductHandler handler) =>
        {
            var request = new Requests.Products.Get();
            var result = await handler.Handle(request);
            return Results.Ok(result);
        })
        .WithName("GetAllProducts")
        .WithTags("Products");
    }
}
