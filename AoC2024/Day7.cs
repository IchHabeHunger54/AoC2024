namespace AoC2024;

public class Day7() : Day(7)
{
    protected override long TaskOne(List<string> lines)
    {
        long result = 0;
        foreach (string line in lines)
        {
            string[] tokens = line.Split(" ");
            long left = long.Parse(tokens[0][..(tokens[0].Length - 1)]);
            long[] right = tokens[1..].Select(long.Parse).ToArray();
            if (CanGiveResultOne(right[0], right[1..], left)) result += left;
        }
        return result;
    }

    protected override long TaskTwo(List<string> lines)
    {
        long result = 0;
        foreach (string line in lines)
        {
            string[] tokens = line.Split(" ");
            long left = long.Parse(tokens[0][..(tokens[0].Length - 1)]);
            long[] right = tokens[1..].Select(long.Parse).ToArray();
            if (CanGiveResultTwo(right[0], right[1..], left)) result += left;
        }
        return result;
    }

    private static bool CanGiveResultOne(long first, long[] others, long result)
    {
        if (first > result) return false;
        if (others.Length == 0) return first == result;
        return CanGiveResultOne(first + others[0], others[1..], result) || CanGiveResultOne(first * others[0], others[1..], result);
    }

    private static bool CanGiveResultTwo(long first, long[] others, long result)
    {
        if (first > result) return false;
        if (others.Length == 0) return first == result;
        return CanGiveResultTwo(first + others[0], others[1..], result)
               || CanGiveResultTwo(first * others[0], others[1..], result)
               || CanGiveResultTwo(first * (long) Math.Pow(10, others[0].ToString().Length) + others[0], others[1..], result);
    }
}