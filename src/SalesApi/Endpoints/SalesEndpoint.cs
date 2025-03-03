using SalesApi.Application.Sales.Commands;
using SalesApi.Application.Sales.Models.Result;
using SalesApi.Application.Sales.Queries;
using SalesApi.Models.Results;

namespace SalesApi.Endpoints;

public class SalesEndpointServices(IMediator mediator,
                                   ILogger<SalesEndpointServices> logger)
{
    public IMediator Mediator { get; set; } = mediator;

    public ILogger<SalesEndpointServices> Logger { get; } = logger;
}

public static class SalesEndpoint
{
    private const string TagEndpoint = "Sales";
    private const string BaseEndpoint = "sales";

    public static void MapSalesEndpoints(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup(BaseEndpoint)
                     .WithTags(TagEndpoint);

        api.MapGet("/", GetSalesAsync);

        api.MapGet("/{id:guid}", GetSaleByIdAsync);

        api.MapPost("/", CreateSaleAsync);

        api.MapDelete("/{id:guid}", CancelSaleByIdAsync);
    }

    private static async Task<Results<Ok<AnySuccessWithDataResult<Sale[]>>,
                              NotFound<AnyFailureResult>,
                              BadRequest<AnyFailureResult>>> GetSalesAsync(
        [AsParameters] SalesEndpointServices services)
    {
        try
        {
            var query = new GetSalesQuery();
            var sales = await services.Mediator.Send(query);

            if (sales is null || sales.Length == 0)
                return TypedResults.NotFound(
                    new AnyFailureResult(HttpStatusCode.NotFound.ToString(),
                                         "Nothing to show",
                                         "We can't find any records at our database. Please, try it again."));

            return TypedResults.Ok(
                new AnySuccessWithDataResult<Sale[]>(HttpStatusCode.OK.ToString(),
                                                     "We found some date here",
                                                     sales));
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(
                new AnyFailureResult(HttpStatusCode.BadRequest.ToString(),
                                     "Oops... something wrong it happend",
                                     ex.Message));
        }
    }

    private static async Task<Results<Ok<AnySuccessWithDataResult<Sale>>,
                              NotFound<AnyFailureResult>,
                              BadRequest<AnyFailureResult>>> GetSaleByIdAsync(
        [FromRoute(Name = "id")] Guid id,
        [AsParameters] SalesEndpointServices services)
    {
        try
        {
            var query = new GetSaleByIdQuery(id);
            var sale = await services.Mediator.Send(query);

            if (sale is null)
                return TypedResults.NotFound(
                    new AnyFailureResult(HttpStatusCode.NotFound.ToString(),
                                         "Nothing to show",
                                         "We can't find any records at our database. Please, try it again."));

            return TypedResults.Ok(
                new AnySuccessWithDataResult<Sale>(HttpStatusCode.OK.ToString(),
                                                   $"We found the sale order {sale.Id}",
                                                   sale));
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(
                new AnyFailureResult(HttpStatusCode.BadRequest.ToString(),
                                     "Oops... something wrong it happend",
                                     ex.Message));
        }
    }

    private static async Task<Results<Created<AnySuccessWithDataResult<Sale>>,
                                      BadRequest<AnyFailureResult>>> CreateSaleAsync(
        [FromBody] CreateSaleCommand command,
        [AsParameters] SalesEndpointServices services)
    {
        try
        {
            var sale = await services.Mediator.Send(command);

            return TypedResults.Created(
                string.Empty,
                new AnySuccessWithDataResult<Sale>(HttpStatusCode.Created.ToString(),
                                                   "Sale has been created",
                                                   sale));
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(
                new AnyFailureResult(HttpStatusCode.BadRequest.ToString(),
                                     "Oops... something wrong it happend",
                                     ex.Message));
        }
    }

    private static async Task<Results<Ok<AnySuccessResult>,
                              NotFound<AnyFailureResult>,
                              BadRequest<AnyFailureResult>>> CancelSaleByIdAsync(
        [FromRoute(Name = "id")] Guid id,
        [AsParameters] SalesEndpointServices services)
    {
        try
        {
            var query = new CancelSaleCommand(id);
            var success = await services.Mediator.Send(query);

            return TypedResults.Ok(
                new AnySuccessResult(HttpStatusCode.OK.ToString(),
                                     $"Sale order id {id} was cancelled"));
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(
                new AnyFailureResult(HttpStatusCode.BadRequest.ToString(),
                                     "Oops... something wrong it happend",
                                     ex.Message));
        }
    }
}

