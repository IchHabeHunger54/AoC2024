namespace AoC2024.util;

public record Position(int x, int y)
{
    public virtual bool Equals(Position? other)
    {
        return other is not null && x == other.x && y == other.y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }

    public static Position operator +(Position a, Position b)
    {
        return new Position(a.x + b.x, a.y + b.y);
    }
}