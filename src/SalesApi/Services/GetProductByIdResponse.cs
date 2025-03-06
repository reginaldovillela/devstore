using SalesApi.Domain.Products.AggregatesModel;

namespace SalesApi.Services;

public record GetProductByIdResponse(bool HasError, 
                                     string ErrorMessage, 
                                     ProductEntity? Product);
