using AoC2024.util;

namespace AoC2024;

public class Day6() : Day(6)
{
    protected override long TaskOne(List<string> lines)
    {
        return GetPositions(lines).ToList().Count - 1;
    }

    protected override long TaskTwo(List<string> lines)
    {
        List<Position> positions = GetPositions(lines).ToList();
        Position guard = GetGuard(lines);
        positions.Remove(guard);
        int result = 0;
        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (!positions.Contains(new Position(j, i))) continue;
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

    private static Position GetGuard(List<string> lines)
    {
        for (int y = 0; y < lines.Count; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (lines[y][x] == '^')
                {
                    return new Position(x, y);
                }
            }
        }
        return new Position(-1, -1);
    }

    private static bool IsLoop(List<string> lines, int guardX, int guardY)
    {
        int sizeX = lines[0].Length;
        int sizeY = lines.Count;
        PositionAndDirection current = new(new Position(guardX, guardY), Direction.Up);
        HashSet<PositionAndDirection> result = [current];
        while (current.Position is { x: > -1, y: > -1 } && current.Position.x < sizeX && current.Position.y < sizeY)
        {
            switch (current.Direction)
            {
                case Direction.Up:
                    if (current.Position.y > 0 && lines[current.Position.y - 1][current.Position.x] is '#' or 'O')
                    {
                        current = current with { Direction = Direction.Right };
                    }
                    else
                    {
                        current = current with { Position = current.Position with { y = current.Position.y - 1 } };
                    }
                    break;
                case Direction.Down:
                    if (current.Position.y < sizeY - 1 && lines[current.Position.y + 1][current.Position.x] is '#' or 'O')
                    {
                        current = current with { Direction = Direction.Left };
                    }
                    else
                    {
                        current = current with { Position = current.Position with { y = current.Position.y + 1 } };
                    }
                    break;
                case Direction.Left:
                    if (current.Position.x > 0 && lines[current.Position.y][current.Position.x - 1] is '#' or 'O')
                    {
                        current = current with { Direction = Direction.Up };
                    }
                    else
                    {
                        current = current with { Position = current.Position with { x = current.Position.x - 1 } };
                    }
                    break;
                case Direction.Right:
                    if (current.Position.x < sizeX - 1 && lines[current.Position.y][current.Position.x + 1] is '#' or 'O')
                    {
                        current = current with { Direction = Direction.Down };
                    }
                    else
                    {
                        current = current with { Position = current.Position with { x = current.Position.x + 1 } };
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

    protected virtual IEnumerable<Position> GetPositions(List<string> lines)
    {
        int sizeX = lines[0].Length;
        int sizeY = lines.Count;
        Position guard = GetGuard(lines);
        Direction direction = Direction.Up;
        HashSet<Position> result = [guard];
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
                        guard = guard with { y = guard.y - 1 };
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
                        guard = guard with { y = guard.y + 1 };
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
                        guard = guard with { x = guard.x - 1 };
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
                        guard = guard with { x = guard.x + 1 };
                        result.Add(guard);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lines));
            }
        }
        return result;
    }
}