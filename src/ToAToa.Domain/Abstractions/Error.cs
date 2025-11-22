namespace ToAToa.Domain.Abstractions;

public record Error(string Code, string? Message = null)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "O resultado especificado é nulo");

    public static Error Unknown(string message) => new ("Error.Unknown", message);

    public override string ToString()
    {
        return Code;
    }
}
