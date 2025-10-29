using BugStore.Handlers.Products;

namespace BugStore.Api.Endpoints.Products;

public static class DeleteProductEndpoint
{
    public static void MapDeleteProductEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:guid}", async (ProductHandler handler, Guid id) =>
        {
            var request = new Requests.Products.Delete { Id = id };
            var result = await handler.Handle(request);
            return result.Success
                ? Results.Ok(result)
                : Results.NotFound(result);
        })
        .WithName("DeleteProduct")
        .WithTags("Products");
    }
}
