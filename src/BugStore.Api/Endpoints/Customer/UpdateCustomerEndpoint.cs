using BugStore.Handlers.Customers;

namespace BugStore.Api.Endpoints.Customer;

public static class UpdateCustomerEndpoint
{
    public static void MapUpdateCustomerEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/customers/{id:guid}", async (CustomerHandler handler, Guid id, Requests.Customers.Update request) =>
        {
            request.Id = id;
            var result = await handler.Handle(request);
            return result is null
                ? Results.NotFound(new { message = "Cliente não encontrado." })
                : Results.Ok(result);
        })
        .WithName("UpdateCustomer")
        .WithTags("Customers");
    }
}
