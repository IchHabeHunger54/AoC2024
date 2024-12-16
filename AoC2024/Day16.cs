using AoC2024.util;

namespace AoC2024;

public class Day16() : Day(16)
{
    protected override long TaskOne(List<string> lines)
    {
        HashSet<Position> positions = [];
        Position start = new(0, 0), end = new(0, 0);
        for (int y = 0; y < lines.Count; y++)
        {
            string line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                char c = line[x];
                if (c != '#')
                {
                    positions.Add(new Position(x, y));
                }
                if (c == 'S')
                {
                    start = new Position(x, y);
                }
                if (c == 'E')
                {
                    end = new Position(x, y);
                }
            }
        }
        return Dijkstra(positions.ToList(), start, end, Direction.Right, out _);
    }

    protected override long TaskTwo(List<string> lines)
    {
        HashSet<Position> positions = [];
        Position start = new(0, 0), end = new(0, 0);
        for (int y = 0; y < lines.Count; y++)
        {
            string line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                char c = line[x];
                if (c != '#')
                {
                    positions.Add(new Position(x, y));
                }
                if (c == 'S')
                {
                    start = new Position(x, y);
                }
                if (c == 'E')
                {
                    end = new Position(x, y);
                }
            }
        }
        return MultiDijkstra(positions.ToList(), start, end).Count;
    }

    private static long Dijkstra(List<Position> input, Position start, Position end, Direction startDirection, out Direction direction)
    {
        Dictionary<Position, long> positions = InitDijkstra(input, start);
        LinkedList<PositionAndDirection> queue = new();
        queue.AddFirst(new PositionAndDirection(start, startDirection));
        direction = startDirection;
        while (queue.Count > 0)
        {
            PositionAndDirection current = queue.First!.Value;
            queue.RemoveFirst();
            if (current.Position == end)
            {
                direction = current.Direction;
                continue;
            }
            Position front = current.Position + GetDirectionStep(current.Direction);
            if (input.Contains(front) && positions[front] >= positions[current.Position])
            {
                queue.AddFirst(current with { Position = front });
                positions[front] = Math.Min(positions[front], positions[current.Position] + 1);
            }
            Position right = current.Position + GetDirectionStep(RotateClockwise(current.Direction));
            if (input.Contains(right) && positions[right] >= positions[current.Position])
            {
                queue.AddLast(new PositionAndDirection(right, RotateClockwise(current.Direction)));
                positions[right] = Math.Min(positions[right], positions[current.Position] + 1001);
            }
            Position left = current.Position + GetDirectionStep(RotateCounterClockwise(current.Direction));
            if (input.Contains(left) && positions[left] >= positions[current.Position])
            {
                queue.AddLast(new PositionAndDirection(left, RotateCounterClockwise(current.Direction)));
                positions[left] = Math.Min(positions[left], positions[current.Position] + 1001);
            }
        }
        return positions[end];
    }

    private static List<Position> MultiDijkstra(List<Position> input, Position start, Position end)
    {
        HashSet<Position> result = [start, end];
        long best = Dijkstra(input, start, end, Direction.Right, out _);
        foreach (Position position in input)
        {
            if (position == start || position == end) continue;
            long l1 = Dijkstra(input, start, position, Direction.Right, out Direction direction);
            long l2 = Dijkstra(input, position, end, direction, out _);
            if (l1 + l2 == best)
            {
                result.Add(position);
            }
            // Heuristic: Only rerun if we're reasonably close.
            else if (l1 + l2 + 3000 > best)
            {
                // Rerun the second Dijkstra for cases where we have a tile accessible from two sides.
                l2 = Dijkstra(input, position, end, GetOpposite(direction), out _);
                if (l1 + l2 == best)
                {
                    result.Add(position);
                }
            }
        }
        return result.ToList();
    }

    private static Position GetDirectionStep(Direction direction)
    {
        int x = direction switch
        {
            Direction.Left => -1,
            Direction.Right => 1,
            _ => 0
        };
        int y = direction switch
        {
            Direction.Up => -1,
            Direction.Down => 1,
            _ => 0
        };
        return new Position(x, y);
    }
    
    private static Direction RotateClockwise(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };
    }
    
    private static Direction RotateCounterClockwise(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Direction.Left,
            Direction.Right => Direction.Up,
            Direction.Down => Direction.Right,
            Direction.Left => Direction.Down,
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };
    }

    private static Direction GetOpposite(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Direction.Down,
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left
        };
    }

    private static Dictionary<Position, long> InitDijkstra(List<Position> input, Position start)
    {
        Dictionary<Position, long> positions = new();
        foreach (Position position in input)
        {
            // We use long.MaxValue / 2 to avoid an overflow.
            positions[position] = long.MaxValue / 2;
        }
        positions[start] = 0;
        return positions;
    }
}