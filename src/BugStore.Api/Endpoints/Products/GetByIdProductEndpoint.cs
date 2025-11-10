using BugStore.Handlers.Products;

namespace BugStore.Api.Endpoints.Products;

public static class GetByIdProductEndpoint
{
    public static void MapGetProductByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async (ProductHandler handler, Guid id) =>
        {
            var request = new Requests.Products.GetById { Id = id };
            var result = await handler.Handle(request);
            return result is null
                ? Results.NotFound(new { message = "Produto não encontrado." })
                : Results.Ok(result);
        })
        .WithName("GetProductById")
        .WithTags("Products");
    }
}
