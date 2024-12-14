namespace ToAToa.Domain.Abstractions;

public class Result
{
    protected Result (bool isSuccess, Error error) : this(isSuccess, error, null)
    {
    }

    protected Result(bool isSuccess, Error error, int? stateCode)
    {
        if ((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
        StateCode = stateCode;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public int? StateCode { get; protected init; }

    public static Result Success() => new(true, Error.None);

    public static Result<TData> Success<TData>(TData data) => new(data, true, Error.None);

    public static Result Failure(Error error) => new(false, error);

    public static Result Failure(Error error, int? statusCode) => new(false, error, statusCode);

    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

    public static Result<TValue> Failure<TValue>(Error error, int? statusCode) => new(default, false, error, statusCode);

    public static Result<TData> Create<TData>(TData? data) =>
        data is not null
            ? Success(data)
            : Failure<TData>(Error.NullValue);
}

public class Result<TData> : Result 
{
    private readonly TData? _data;
    protected internal Result(TData? data, bool isSuccess, Error error) : this(data, isSuccess, error, null) 
    {
    }

    protected internal Result(TData? data, bool isSuccess, Error error, int? stateCode) : base(isSuccess, error)
    {
        _data = data;
        StateCode = stateCode;
    }

    public TData? Data =>
        IsSuccess
            ? _data
            : default;

    public static implicit operator Result<TData>(TData? data)
    {
        return Create(data);
    }
}
