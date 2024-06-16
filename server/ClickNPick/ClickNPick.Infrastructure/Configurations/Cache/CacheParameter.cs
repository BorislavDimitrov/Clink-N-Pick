namespace ClickNPick.Application.Configurations.Cache;

public class CacheParameter
{
    private readonly string? parameterName;
    private readonly object? parameterValue;

    public CacheParameter(object? parameterValue)
        => this.parameterValue = parameterValue;

    public CacheParameter(string? parameterName, object? parameterValue)
        : this(parameterValue)
            => this.parameterName = parameterName;

    public override string ToString()
        => $"{parameterName}:{parameterValue}";
}
