namespace ProHealth.Shared.Models;

public class FieldValue : IEquatable<FieldValue>
{
    public required string Field { get; set; }

    public string? Value { get; set; } = string.Empty;

    public bool Equals(FieldValue? other)
    {
        if (other is null)
        {
            return false;
        }

        if (Field != other.Field)
        {
            return false;
        }

        if (Value != other.Value)
        {
            return false;
        }

        return true;
    }
}
