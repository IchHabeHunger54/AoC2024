namespace AoC2024;

public class Day10() : Day(10)
{
    protected override long TaskOne(List<string> lines)
    {
        List<List<int>> grid = ToListListInt(lines);
        int result = 0;
        for (int y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].Count; x++)
            {
                if (grid[y][x] != 0) continue;
                HashSet<(int, int)> reached = [];
                GetTrailScore(grid, reached, y, x);
                result += reached.Count;
            }
        }
        return result;
    }

    protected override long TaskTwo(List<string> lines)
    {
        List<List<int>> grid = ToListListInt(lines);
        int result = 0;
        for (int y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].Count; x++)
            {
                if (grid[y][x] != 0) continue;
                result += GetTrailScore(grid, [], y, x);
            }
        }
        return result;
    }

    private static List<List<int>> ToListListInt(List<string> lines)
    {
        List<List<int>> result = [];
        foreach (string line in lines)
        {
            List<int> list = [];
            list.AddRange(line.Select(c => c - '0'));
            result.Add(list);
        }
        return result;
    }
    
    private static int GetTrailScore(List<List<int>> grid, HashSet<(int x, int y)> reached, int y, int x)
    {
        int current = grid[y][x];
        if (current == 9)
        {
            reached.Add((x, y));
            return 1;
        }
        int next = current + 1;
        int result = 0;
        // up
        if (y > 0 && grid[y - 1][x] == next)
        {
            result += GetTrailScore(grid, reached, y - 1, x);
        }
        // down
        if (y < grid.Count - 1 && grid[y + 1][x] == next)
        {
            result += GetTrailScore(grid, reached, y + 1, x);
        }
        // left
        if (x > 0 && grid[y][x - 1] == next)
        {
            result += GetTrailScore(grid, reached, y, x - 1);
        }
        // right
        if (x < grid[y].Count - 1 && grid[y][x + 1] == next)
        {
            result += GetTrailScore(grid, reached, y, x + 1);
        }
        return result;
    }
}