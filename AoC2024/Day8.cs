namespace AoC2024;

public class Day8() : Day(8)
{
    protected override long TaskOne(List<string> lines)
    {
        int sizeX = lines[0].Length;
        int sizeY = lines.Count;
        Dictionary<char, List<(int x, int y)>> dict = GetAntennae(lines);
        HashSet<(int x, int y)> points = [];
        foreach (List<(int x, int y)> list in dict.Keys.Select(c => dict[c]))
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                (int x, int y) current = list[i];
                for (int j = i + 1; j < list.Count; j++)
                {
                    (int x, int y) next = list[j];
                    int diffX = next.x - current.x;
                    int diffY = next.y - current.y;
                    int x = current.x - diffX;
                    int y = current.y - diffY;
                    if (x >= 0 && x < sizeX && y >= 0 && y < sizeY)
                    {
                        points.Add((x, y));
                    }
                    x = next.x + diffX;
                    y = next.y + diffY;
                    if (x >= 0 && x < sizeX && y >= 0 && y < sizeY)
                    {
                        points.Add((x, y));
                    }
                }
            }
        }
        return points.Count;
    }

    protected override long TaskTwo(List<string> lines)
    {
        int sizeX = lines[0].Length;
        int sizeY = lines.Count;
        Dictionary<char, List<(int x, int y)>> dict = GetAntennae(lines);
        HashSet<(int x, int y)> points = [];
        foreach (List<(int x, int y)> list in dict.Keys.Select(c => dict[c]))
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                (int x, int y) current = list[i];
                points.Add(current);
                for (int j = i + 1; j < list.Count; j++)
                {
                    (int x, int y) next = list[j];
                    points.Add(next);
                    int diffX = next.x - current.x;
                    int diffY = next.y - current.y;
                    int x = current.x - diffX;
                    int y = current.y - diffY;
                    while (x >= 0 && x < sizeX && y >= 0 && y < sizeY)
                    {
                        points.Add((x, y));
                        x -= diffX;
                        y -= diffY;
                    }
                    x = next.x + diffX;
                    y = next.y + diffY;
                    while (x >= 0 && x < sizeX && y >= 0 && y < sizeY)
                    {
                        points.Add((x, y));
                        x += diffX;
                        y += diffY;
                    }
                }
            }
        }
        return points.Count;
    }

    private static Dictionary<char, List<(int x, int y)>> GetAntennae(List<string> lines)
    {
        int sizeX = lines[0].Length;
        int sizeY = lines.Count;
        Dictionary<char, List<(int x, int y)>> dict = new();
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                char c = lines[y][x];
                if (!char.IsAsciiLetterOrDigit(c)) continue;
                if (!dict.ContainsKey(c))
                {
                    dict.Add(c, []);
                }
                dict[c].Add((x, y));
            }
        }
        return dict;
    }
}