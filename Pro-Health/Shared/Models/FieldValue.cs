namespace ProHealth.Shared.Models;

public class FieldValue
{
    public required string Field { get; set; }

    public string? Value { get; set; } = string.Empty;
}
