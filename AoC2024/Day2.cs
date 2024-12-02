namespace AoC2024;

public class Day2() : Day(2)
{
    protected override int TaskOne(List<string> lines)
    {
        List<List<int>> grid = lines.Select(x => x.Split(' ').Select(int.Parse).ToList()).ToList();
        bool[] result = new bool[grid.Count];
        for (int i = 0; i < grid.Count; i++)
        {
            List<int> row = grid[i];
            result[i] = row.SequenceEqual(row.OrderBy(x => x)) || row.SequenceEqual(row.OrderByDescending(x => x));
            for (int j = 0; j < row.Count - 1; j++)
            {
                result[i] &= Math.Abs(row[j] - row[j + 1]) is > 0 and <= 3;
            }
        }
        return result.Select(x => x ? 1 : 0).Sum();
    }

    protected override int TaskTwo(List<string> lines)
    {
        throw new NotImplementedException();
    }
}