using BugStore.Handlers.Customers;

namespace BugStore.Api.Endpoints.Customer;

public static class GetCustomerEndpoint
{
    public static void MapGetCustomersEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/customers", async (CustomerHandler handler) =>
        {
            var request = new Requests.Customers.Get();
            var result = await handler.Handle(request);
            return Results.Ok(result);
        })
        .WithName("GetAllCustomers")
        .WithTags("Customers");
    }
}
