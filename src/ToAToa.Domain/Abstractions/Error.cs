namespace ToAToa.Domain.Abstractions;

public class Error(string code, string? message = null) : IEquatable<Error>
{
    private string Code { get; } = code;
    private string? Message { get; } = message;

    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "O resultado especificado é nulo");

    public static Error Unknown(string message) => new ("Error.Unknown", message);

    public bool Equals(Error? other)
    {
        if (other is null)
        {
            return false;
        }

        return Code == other.Code && Message == other.Message;
    }

    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Error? a, Error? b)
    {
        return !(a == b);
    }

    public override bool Equals(object? obj)
    {
        return obj is Error error && Equals(error);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Message);
    }

    public override string ToString()
    {
        return Code;
    }
}
