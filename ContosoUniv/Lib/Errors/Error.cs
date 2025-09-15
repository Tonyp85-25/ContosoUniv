namespace ContosoUniv.Lib.Errors;

public enum ErrorType
{
    NotFound,
    Unauthorized,
    Validation,
    AlreadyExists,
    DbUpdate,
    None
}
public sealed record Error(ErrorType Code, string Description)
{
    public static readonly Error None = new(ErrorType.None, string.Empty);
}