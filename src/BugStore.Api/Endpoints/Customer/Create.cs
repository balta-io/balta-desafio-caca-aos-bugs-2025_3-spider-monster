
using BugStore.Handlers.Customers;

namespace BugStore.Api.Endpoints.Customer;

public static class Create
{
    public static void MapCreateCustomerEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/customers", async (CustomerHandler handler, Requests.Customers.Create request) =>
        {
            var result = await handler.Handle(request);
            return Results.Created($"/customers/{result.Id}", result);
        })
        .WithName("CreateCustomer")
        .WithTags("Customers");
    }
}
