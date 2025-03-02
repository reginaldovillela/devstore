namespace SalesApi.Models.Results;

public record AnySuccessWithDataResult<T>(string Status, 
                                          string Message, 
                                          T Data) 
    : AnySuccessResult(Status, Message);

