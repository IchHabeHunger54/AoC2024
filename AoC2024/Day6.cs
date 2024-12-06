namespace AoC2024;

public class Day6() : Day(6)
{
    protected override int TaskOne(List<string> lines)
    {
        int sizeX = lines[0].Length;
        int sizeY = lines.Count;
        (int x, int y) guard = (-1, -1);
        Direction direction = Direction.Up;
        for (int y = 0; y < lines.Count; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (lines[y][x] == '^')
                {
                    guard = (x, y);
                }
            }
        }
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
                    throw new ArgumentOutOfRangeException();
            }
        }
        return result.Count - 1;
    }

    protected override int TaskTwo(List<string> lines)
    {
        throw new NotImplementedException();
    }

    private enum Direction
    {
        Up, Down, Left, Right
    }
}