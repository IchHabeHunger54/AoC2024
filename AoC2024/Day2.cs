namespace AoC2024;

public class Day2() : Day(2)
{
    protected override long TaskOne(List<string> lines)
    {
        return lines.Select(x => x.Split(' ').Select(int.Parse).ToList()).Sum(t => CheckRow(t) ? 1 : 0);
    }

    protected override long TaskTwo(List<string> lines)
    {
        List<List<int>> grid = lines.Select(x => x.Split(' ').Select(int.Parse).ToList()).ToList();
        int result = 0;
        foreach (List<int> row in grid)
        {
            if (CheckRow(row))
            {
                result++;
            }
            else
            {
                int problems = 0;
                for (int i = 0; i < row.Count; i++)
                {
                    List<int> copy = row.ToList();
                    copy.RemoveAt(i);
                    problems += CheckRow(copy) ? 1 : 0;
                }
                result += problems > 0 ? 1 : 0;
            }
        }
        return result;
    }

    private static bool CheckRow(List<int> row)
    {
        bool result = row.SequenceEqual(row.OrderBy(x => x)) || row.SequenceEqual(row.OrderByDescending(x => x));
        for (int j = 0; j < row.Count - 1; j++)
        {
            result &= Math.Abs(row[j] - row[j + 1]) is > 0 and <= 3;
        }
        return result;
    }
}