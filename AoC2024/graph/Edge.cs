namespace AoC2024.graph;

public record Edge<T>(Node<T> From, Node<T> To, int Weight, bool OneWay)
{
    public virtual bool Equals(Edge<T>? other)
    {
        if (other == null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (other.Weight != Weight) return false;
        if (other.OneWay != OneWay) return false;
        if (OneWay) return From == other.From && To == other.To;
        return (From == other.From && To == other.To) || (From == other.To && To == other.From);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(From, To, Weight, OneWay);
    }

    public Node<T> GetOpposite(Node<T> node)
    {
        return node == From ? To : From;
    }
}