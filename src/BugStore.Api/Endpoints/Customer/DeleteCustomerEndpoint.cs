using BugStore.Handlers.Customers;

namespace BugStore.Api.Endpoints.Customer;

public static class DeleteCustomerEndpoint
{
    public static void MapDeleteCustomerEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/customers/{id:guid}", async (CustomerHandler handler, Guid id) =>
        {
            var request = new Requests.Customers.Delete { Id = id };
            var result = await handler.Handle(request);
            return result.Success
                ? Results.Ok(result)
                : Results.NotFound(result);
        })
        .WithName("DeleteCustomer")
        .WithTags("Customers");
    }
}
