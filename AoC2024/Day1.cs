namespace AoC2024;

public class Day1() : Day(1)
{
    protected override int TaskOne(List<string> lines)
    {
        List<string[]> data = lines.Select(x => x.Split("   ")).ToList();
        List<int> left = data.Select(x => x[0]).Select(int.Parse).ToList();
        List<int> right = data.Select(x => x[1]).Select(int.Parse).ToList();
        left.Sort(); 
        right.Sort();
        return left.Select((t, i) => Math.Abs(t - right[i])).Sum();
    }

    protected override int TaskTwo(List<string> lines)
    {
        List<string[]> data = lines.Select(x => x.Split("   ")).ToList();
        List<int> left = data.Select(x => x[0]).Select(int.Parse).ToList();
        List<int> right = data.Select(x => x[1]).Select(int.Parse).ToList();
        return left.Select(x => right.Where(y => y == x).Sum()).Sum();
    }
}