using BugStore.Handlers.Products;

namespace BugStore.Api.Endpoints.Products;

public static class UpdateProductEndpoint
{
    public static void MapUpdateProductEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id:guid}", async (ProductHandler handler, Guid id, Requests.Products.Update request) =>
        {
            request.Id = id;
            var result = await handler.Handle(request);
            return result is null
                ? Results.NotFound(new { message = "Produto não encontrado." })
                : Results.Ok(result);
        })
        .WithName("UpdateProduct")
        .WithTags("Products");
    }
}
