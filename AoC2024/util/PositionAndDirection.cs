namespace AoC2024.util;

public record PositionAndDirection(Position Position, Direction Direction)
{
    public virtual bool Equals(PositionAndDirection? other)
    {
        return other != null && Position == other.Position && Direction == other.Direction;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Position, (int) Direction);
    }
}