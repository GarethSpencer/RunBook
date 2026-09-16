namespace Utilities.Models.Results;

public record EnumResult
{
    public required int Value { get; init; }
    public required string Name { get; init; }
}
