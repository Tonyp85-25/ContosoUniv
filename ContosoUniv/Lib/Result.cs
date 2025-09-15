using ContosoUniv.Lib.Errors;

namespace ContosoUniv.Lib;

public record Result
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
       
    }
    
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);
}
public record Result<T> :Result
{
    private Result(bool isSuccess, Error error,T? value):base(isSuccess, error)
    {
        _data = value;
    }
    
    private readonly T? _data; 

    public T? GetData()
    {
        return _data;
    }
    

    public static Result<T> Success(T? value) => new(true, Error.None,value);
    

}