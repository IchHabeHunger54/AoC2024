namespace AoC2024.graph;

public record Node<T>(T Value) : IComparable<Node<T>> where T : IComparable<T>
{
    public int CompareTo(Node<T>? other)
    {
        ArgumentNullException.ThrowIfNull(other);
        return Value.CompareTo(other.Value);
    }
}