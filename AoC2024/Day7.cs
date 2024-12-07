namespace AoC2024;

public class Day7() : Day(7)
{
    protected override int TaskOne(List<string> lines)
    {
        long result = (from line in lines select line.Split(" ") into tokens let left = long.Parse(tokens[0][..(tokens[0].Length - 1)]) let right = tokens[1..].Select(long.Parse).ToArray() where CanGiveResultOne(right[0], right[1..], left) select left).Sum();
        if (result > int.MaxValue)
        {
            Console.WriteLine($"Long value result: {result}");
        }
        return (int) result;
    }

    private static bool CanGiveResultOne(long first, long[] others, long result)
    {
        if (first > result) return false;
        if (others.Length == 0) return first == result;
        return CanGiveResultOne(first + others[0], others[1..], result) || CanGiveResultOne(first * others[0], others[1..], result);
    }

    protected override int TaskTwo(List<string> lines)
    {
        throw new NotImplementedException();
    }
}