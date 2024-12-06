namespace AoC2024;

public class Day6() : Day(6)
{
    protected override int TaskOne(List<string> lines)
    {
        return GetPositions(lines).ToList().Count - 1;
    }

    protected override int TaskTwo(List<string> lines)
    {
        List<(int x, int y)> positions = GetPositions(lines).ToList();
        (int x, int y) guard = GetGuard(lines);
        positions.Remove(guard);
        int result = 0;
        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (!positions.Contains((j, i))) continue;
                if (lines[i][j] != '.') continue;
                lines[i] = lines[i][..j] + 'O' + lines[i][(j + 1)..];
                if (IsLoop(lines, guard.x, guard.y))
                {
                    result++;
                }
                lines[i] = lines[i][..j] + '.' + lines[i][(j + 1)..];
            }
        }
        return result;
    }

    private static (int x, int y) GetGuard(List<string> lines)
    {
        for (int y = 0; y < lines.Count; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (lines[y][x] == '^')
                {
                    return (x, y);
                }
            }
        }
        return (-1, -1);
    }

    private static bool IsLoop(List<string> lines, int guardX, int guardY)
    {
        int sizeX = lines[0].Length;
        int sizeY = lines.Count;
        PositionAndDirection current = new(guardX, guardY, Direction.Up);
        HashSet<PositionAndDirection> result = [current];
        while (current is { X: > -1, Y: > -1 } && current.X < sizeX && current.Y < sizeY)
        {
            switch (current.Direction)
            {
                case Direction.Up:
                    if (current.Y > 0 && lines[current.Y - 1][current.X] is '#' or 'O')
                    {
                        current = current with { Direction = Direction.Right };
                    }
                    else
                    {
                        current = current with { Y = current.Y - 1 };
                    }
                    break;
                case Direction.Down:
                    if (current.Y < sizeY - 1 && lines[current.Y + 1][current.X] is '#' or 'O')
                    {
                        current = current with { Direction = Direction.Left };
                    }
                    else
                    {
                        current = current with { Y = current.Y + 1 };
                    }
                    break;
                case Direction.Left:
                    if (current.X > 0 && lines[current.Y][current.X - 1] is '#' or 'O')
                    {
                        current = current with { Direction = Direction.Up };
                    }
                    else
                    {
                        current = current with { X = current.X - 1 };
                    }
                    break;
                case Direction.Right:
                    if (current.X < sizeX - 1 && lines[current.Y][current.X + 1] is '#' or 'O')
                    {
                        current = current with { Direction = Direction.Down };
                    }
                    else
                    {
                        current = current with { X = current.X + 1 };
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lines));
            }
            if (!result.Add(current))
            {
                return true;
            }
        }
        return false;
    }

    private static IEnumerable<(int x, int y)> GetPositions(List<string> lines)
    {
        int sizeX = lines[0].Length;
        int sizeY = lines.Count;
        (int x, int y) guard = GetGuard(lines);
        Direction direction = Direction.Up;
        HashSet<(int x, int y)> result = [guard];
        while (guard is { x: > -1, y: > -1 } && guard.x < sizeX && guard.y < sizeY)
        {
            switch (direction)
            {
                case Direction.Up:
                    if (guard.y > 0 && lines[guard.y - 1][guard.x] == '#')
                    {
                        direction = Direction.Right;
                    }
                    else
                    {
                        guard = (guard.x, guard.y - 1);
                        result.Add(guard);
                    }
                    break;
                case Direction.Down:
                    if (guard.y < sizeY - 1 && lines[guard.y + 1][guard.x] == '#')
                    {
                        direction = Direction.Left;
                    }
                    else
                    {
                        guard = (guard.x, guard.y + 1);
                        result.Add(guard);
                    }
                    break;
                case Direction.Left:
                    if (guard.x > 0 && lines[guard.y][guard.x - 1] == '#')
                    {
                        direction = Direction.Up;
                    }
                    else
                    {
                        guard = (guard.x - 1, guard.y);
                        result.Add(guard);
                    }
                    break;
                case Direction.Right:
                    if (guard.x < sizeX - 1 && lines[guard.y][guard.x + 1] == '#')
                    {
                        direction = Direction.Down;
                    }
                    else
                    {
                        guard = (guard.x + 1, guard.y);
                        result.Add(guard);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lines));
            }
        }
        return result;
    }
    
    private record PositionAndDirection(int X, int Y, Direction Direction)
    {
        public virtual bool Equals(PositionAndDirection? other)
        {
            return other != null && X == other.X && Y == other.Y && Direction == other.Direction;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, (int) Direction);
        }
    }

    private enum Direction
    {
        Up, Down, Left, Right
    }
}