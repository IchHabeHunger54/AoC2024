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
                List<(int x, int y)> points = FloodFill(lines, j, i);
                long perimeter = 0;
                foreach ((int x, int y) point in points)
                {
                    handled[point.y][point.x] = true;
                    if (point.x == 0 || !points.Contains((point.x - 1, point.y)))
                    {
                        perimeter++;
                    }
                    if (point.x == lines[0].Length - 1 || !points.Contains((point.x + 1, point.y)))
                    {
                        perimeter++;
                    }
                    if (point.y == 0 || !points.Contains((point.x, point.y - 1)))
                    {
                        perimeter++;
                    }
                    if (point.y == lines.Count - 1 || !points.Contains((point.x, point.y + 1)))
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
        throw new NotImplementedException();
    }

    private static List<(int x, int y)> FloodFill(List<string> lines, int startX, int startY)
    {
        char letter = lines[startY][startX];
        List<(int x, int y)> result = [];
        HashSet<(int x, int y)> visited = [];
        Queue<(int x, int y)> queue = new();
        queue.Enqueue((startX, startY));
        while (queue.Count > 0)
        {
            (int x, int y) current = queue.Dequeue();
            if (lines[current.y][current.x] != letter) continue;
            result.Add(current);
            visited.Add(current);
            if (current.x > 0)
            {
                (int x, int y) newPoint = (current.x - 1, current.y);
                if (!visited.Contains(newPoint))
                {
                    queue.Enqueue(newPoint);
                    visited.Add(newPoint);
                }
            }
            if (current.x < lines[0].Length - 1)
            {
                (int x, int y) newPoint = (current.x + 1, current.y);
                if (!visited.Contains(newPoint))
                {
                    queue.Enqueue(newPoint);
                    visited.Add(newPoint);
                }
            }
            if (current.y > 0)
            {
                (int x, int y) newPoint = (current.x, current.y - 1);
                if (!visited.Contains(newPoint))
                {
                    queue.Enqueue(newPoint);
                    visited.Add(newPoint);
                }
            }
            if (current.y < lines.Count - 1)
            {
                (int x, int y) newPoint = (current.x, current.y + 1);
                if (!visited.Contains(newPoint))
                {
                    queue.Enqueue(newPoint);
                    visited.Add(newPoint);
                }
            }
        }
        return result;
    }
}