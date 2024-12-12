using AoC2024.util;

namespace AoC2024;

public class Day12() : Day(12)
{
    protected override long TaskOne(List<string> lines)
    {
        bool[][] handled = new bool[lines.Count][];
        for (int i = 0; i < lines.Count; i++)
        {
            handled[i] = new bool[lines[i].Length];
        }
        long result = 0;
        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (handled[i][j]) continue;
                List<Position> points = FloodFill(lines, j, i);
                long perimeter = 0;
                foreach (Position point in points)
                {
                    handled[point.y][point.x] = true;
                    if (point.x == 0 || !points.Contains(point with { x = point.x - 1 }))
                    {
                        perimeter++;
                    }
                    if (point.x == lines[0].Length - 1 || !points.Contains(point with { x = point.x + 1 }))
                    {
                        perimeter++;
                    }
                    if (point.y == 0 || !points.Contains(point with { y = point.y - 1 }))
                    {
                        perimeter++;
                    }
                    if (point.y == lines.Count - 1 || !points.Contains(point with { y = point.y + 1 }))
                    {
                        perimeter++;
                    }
                }
                result += points.Count * perimeter;
            }
        }
        return result;
    }

    protected override long TaskTwo(List<string> lines)
    {
        bool[][] handled = new bool[lines.Count][];
        for (int i = 0; i < lines.Count; i++)
        {
            handled[i] = new bool[lines[i].Length];
        }
        long result = 0;
        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (handled[i][j]) continue;
                List<Position> points = FloodFill(lines, j, i);
                Dictionary<Position, Direction> horizontalEdges = [];
                Dictionary<Position, Direction> verticalEdges = [];
                foreach (Position point in points)
                {
                    handled[point.y][point.x] = true;
                    Position newPoint = point with { x = point.x - 1 };
                    if (point.x == 0 || !points.Contains(newPoint))
                    {
                        horizontalEdges[newPoint] = Direction.Left;
                    }
                    newPoint = point with { x = point.x + 1 };
                    if (point.x == lines[0].Length - 1 || !points.Contains(newPoint))
                    {
                        horizontalEdges[point] = Direction.Right;
                    }
                    newPoint = point with { y = point.y - 1 };
                    if (point.y == 0 || !points.Contains(newPoint))
                    {
                        verticalEdges[newPoint] = Direction.Up;
                    }
                    newPoint = point with { y = point.y + 1 };
                    if (point.y == lines.Count - 1 || !points.Contains(newPoint))
                    {
                        verticalEdges[point] = Direction.Down;
                    }
                }
                long perimeter = 0;
                List<Position> sortedEdges;
                Position? current;
                Direction? currentDirection;
                // iterate horizontally
                sortedEdges = horizontalEdges.Keys.ToList();
                sortedEdges.Sort((a, b) =>
                {
                    int dx = a.x.CompareTo(b.x);
                    return dx != 0 ? dx : a.y.CompareTo(b.y);
                });
                current = null;
                currentDirection = null;
                foreach (Position edge in sortedEdges)
                {
                    if (current == null || currentDirection == null || edge != current with { y = current.y + 1 } || horizontalEdges[edge] != currentDirection)
                    {
                        perimeter++;
                    }
                    current = edge;
                    currentDirection = horizontalEdges[current];
                }
                // iterate vertically
                sortedEdges = verticalEdges.Keys.ToList();
                sortedEdges.Sort((a, b) =>
                {
                    int dy = a.y.CompareTo(b.y);
                    return dy != 0 ? dy : a.x.CompareTo(b.x);
                });
                current = null;
                currentDirection = null;
                foreach (Position edge in sortedEdges)
                {
                    if (current == null || currentDirection == null || edge != current with { x = current.x + 1 } || verticalEdges[edge] != currentDirection)
                    {
                        perimeter++;
                    }
                    current = edge;
                    currentDirection = verticalEdges[current];
                }
                result += points.Count * perimeter;
            }
        }
        return result;
    }

    private static List<Position> FloodFill(List<string> lines, int startX, int startY)
    {
        char letter = lines[startY][startX];
        List<Position> result = [];
        HashSet<Position> visited = [];
        Queue<Position> queue = new();
        queue.Enqueue(new Position(startX, startY));
        while (queue.Count > 0)
        {
            Position current = queue.Dequeue();
            if (lines[current.y][current.x] != letter) continue;
            result.Add(current);
            visited.Add(current);
            if (current.x > 0)
            {
                Position newPoint = current with { x = current.x - 1 };
                if (!visited.Contains(newPoint))
                {
                    queue.Enqueue(newPoint);
                    visited.Add(newPoint);
                }
            }
            if (current.x < lines[0].Length - 1)
            {
                Position newPoint = current with { x = current.x + 1 };
                if (!visited.Contains(newPoint))
                {
                    queue.Enqueue(newPoint);
                    visited.Add(newPoint);
                }
            }
            if (current.y > 0)
            {
                Position newPoint = current with { y = current.y - 1 };
                if (!visited.Contains(newPoint))
                {
                    queue.Enqueue(newPoint);
                    visited.Add(newPoint);
                }
            }
            if (current.y < lines.Count - 1)
            {
                Position newPoint = current with { y = current.y + 1 };
                if (!visited.Contains(newPoint))
                {
                    queue.Enqueue(newPoint);
                    visited.Add(newPoint);
                }
            }
        }
        return result;
    }

    private enum Direction
    {
        Up, Down, Left, Right
    }
}