using SalesApi.Application.Products.Commands;
using SalesApi.Application.Products.Models.Result;
using SalesApi.Application.Products.Queries;
using SalesApi.Models.Results;

namespace SalesApi.Endpoints;

public class ProductsEndpointServices(IMediator mediator,
                                      ILogger<ProductsEndpointServices> logger)
{
    public IMediator Mediator { get; set; } = mediator;

    public ILogger<ProductsEndpointServices> Logger { get; } = logger;
}

public static class ProductsEndpoint
{
    private const string TagEndpoint = "Products";
    private const string BaseEndpoint = "products";

    public static void MapProductsEndpoints(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup(BaseEndpoint)
                     .WithTags(TagEndpoint);

        api.MapGet("/", GetProductsAsync);

        api.MapPost("/", CreateProductAsync);
    }

    private static async Task<Results<Ok<Product[]>,
                              NotFound<AnyFailureResult>,
                              BadRequest<AnyFailureResult>>> GetProductsAsync(
        [AsParameters] ProductsEndpointServices services)
    {
        try
        {
            var query = new GetProductsQuery();
            var products = await services.Mediator.Send(query);

            if (products is null || products.Length == 0)
                return TypedResults.NotFound(
                    new AnyFailureResult(HttpStatusCode.NotFound.ToString(), 
                                         "Nothing to show", 
                                         "We can't find any records at our database. Please, try it again."));

            return TypedResults.Ok(products);
        }
        catch (Exception ex)
        {
            return TypedResults.BadRequest(
                new AnyFailureResult(HttpStatusCode.BadRequest.ToString(),
                                     "Oops... something wrong it happend",
                                     ex.Message));
        }
    }

    private static async Task<Results<Created<AnySuccessWithDataResult<Product>>,
                                      BadRequest<AnyFailureResult>>> CreateProductAsync(
        [FromBody] CreateProductCommand command,
        [AsParameters] ProductsEndpointServices services)
    {
        try
        {
            var product = await services.Mediator.Send(command);

            return TypedResults.Created(
                string.Empty, 
                new AnySuccessWithDataResult<Product>(HttpStatusCode.Created.ToString(),
                                                      "Product created", 
                                                      product));
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

