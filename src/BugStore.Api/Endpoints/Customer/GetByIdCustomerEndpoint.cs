using BugStore.Handlers.Customers;

namespace BugStore.Api.Endpoints.Customer;

public static class GetByIdCustomerEndpoint
{
    public static void MapGetCustomerByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/customers/{id:guid}", async (CustomerHandler handler, Guid id) =>
        {
            var request = new Requests.Customers.GetById { Id = id };
            var result = await handler.Handle(request);
            return result is null
                ? Results.NotFound(new { message = "Cliente não encontrado." })
                : Results.Ok(result);
        })
        .WithName("GetCustomerById")
        .WithTags("Customers");
    }
}
