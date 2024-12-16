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
        // We start at the "entrance" left to the start due to the Dijkstra impl, and remove that 1 extra move at the end.
        return Dijkstra(positions, start + new Position(-1, 0), end) - 1;
    }

    protected override long TaskTwo(List<string> lines)
    {
        throw new NotImplementedException();
    }

    private static long Dijkstra(HashSet<Position> input, Position start, Position end)
    {
        Dictionary<Position, long> positions = new();
        foreach (Position position in input)
        {
            // We use long.MaxValue / 2 to avoid an overflow.
            positions[position] = long.MaxValue / 2;
        }
        positions[start] = 0;
        HashSet<PositionAndDirection> visited = [];
        LinkedList<PositionAndDirection> queue = new();
        queue.AddFirst(new PositionAndDirection(start, Direction.Right));
        while (queue.Count > 0)
        {
            PositionAndDirection current = queue.First!.Value;
            queue.RemoveFirst();
            if (current.Position == end) continue;
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
}