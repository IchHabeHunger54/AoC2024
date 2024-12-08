namespace AoC2024;

public class Day8() : Day(8)
{
    protected override long TaskOne(List<string> lines)
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
                    int x1 = current.x - diffX;
                    int x2 = next.x + diffX;
                    int y1 = current.y - diffY;
                    int y2 = next.y + diffY;
                    if (!(x1 < 0 || x1 >= sizeX || y1 < 0 || y1 >= sizeY))
                    {
                        points.Add((x1, y1));
                    }
                    if (!(x2 < 0 || x2 >= sizeX || y2 < 0 || y2 >= sizeY))
                    {
                        points.Add((x2, y2));
                    }
                }
            }
        }
        return points.Count;
    }

    protected override long TaskTwo(List<string> lines)
    {
        throw new NotImplementedException();
    }
}