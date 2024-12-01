namespace AoC2024;

public class Day1() : Day(1)
{
    protected override int TaskOne(List<string> lines)
    {
        List<string[]> data = lines.Select(x => x.Split("   ")).ToList();
        List<string> left = data.Select(x => x[0]).ToList();
        List<string> right = data.Select(x => x[1]).ToList();
        left.Sort(); 
        right.Sort();
        return left.Select((t, i) => Math.Abs(int.Parse(t) - int.Parse(right[i]))).Sum();
    }

    protected override int TaskTwo(List<string> lines)
    {
        throw new NotImplementedException();
    }
}