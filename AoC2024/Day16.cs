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
        return Dijkstra(positions.ToList(), start, end).cost;
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
        return Dijkstra(positions.ToList(), start, end).paths.SelectMany(e => e).Distinct().Count();
    }

    private static (long cost, List<List<Position>> paths) Dijkstra(List<Position> input, Position start, Position end)
    {
        Queue<Node> queue = new();
        queue.Enqueue(new Node(start, Direction.Right, 0, []));
        HashSet<PositionAndDirection> visited = [];
        long maxCost = long.MaxValue;
        List<List<Position>> paths = [];
        while (queue.Count > 0)
        {
            Node node = queue.Dequeue();
            if (node.Cost > maxCost) break;
            visited.Add(node.ToPositionAndDirection());
            if (node.Position == end)
            {
                maxCost = node.Cost;
                paths.Add(node.Path);
                continue;
            }
            Position front = node.Position + GetDirectionStep(node.Direction);
            if (input.Contains(front) && !visited.Contains(new PositionAndDirection(front, node.Direction)))
            {
                queue.Enqueue(new Node(front, node.Direction, node.Cost + 1, [..node.Path, front]));
            }
            Position left = node.Position + GetDirectionStep(RotateCounterClockwise(node.Direction));
            if (input.Contains(left) && !visited.Contains(new PositionAndDirection(left, RotateCounterClockwise(node.Direction))))
            {
                queue.Enqueue(new Node(left, RotateCounterClockwise(node.Direction), node.Cost + 1001, [..node.Path, left]));
            }
            Position right = node.Position + GetDirectionStep(RotateClockwise(node.Direction));
            if (input.Contains(right) && !visited.Contains(new PositionAndDirection(right, RotateClockwise(node.Direction))))
            {
                queue.Enqueue(new Node(right, RotateClockwise(node.Direction), node.Cost + 1001, [..node.Path, right]));
            }
        }
        return (maxCost, paths);
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

    private record Node(Position Position, Direction Direction, long Cost, List<Position> Path)
    {
        public PositionAndDirection ToPositionAndDirection()
        {
            return new PositionAndDirection(Position, Direction);
        }
    }
}