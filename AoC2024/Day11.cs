namespace AoC2024;

public class Day11() : Day(11)
{
    protected override long TaskOne(List<string> lines)
    {
        return DoTask(lines, 25);
    }

    protected override long TaskTwo(List<string> lines)
    {
        return DoTask(lines, 75);
    }

    private static long DoTask(List<string> lines, int runs)
    {
        List<long> numbers = lines[0].Split(' ').Select(long.Parse).ToList();
        List<Thread> threads = [];
        List<long> results = [];
        for (int i = 0; i < numbers.Count; i++)
        {
            results.Add(0);
            int j = i;
            Thread thread = new(() => results[j] = RecursiveStep(numbers[j], 0, runs));
            threads.Add(thread);
            thread.Start();
        }
        foreach (Thread thread in threads)
        {
            thread.Join();
        }
        return results.Sum();
    }

    private static long RecursiveStep(long number, int step, int maxStep)
    {
        if (step == maxStep) return 1;
        if (number == 0) return RecursiveStep(1, step + 1, maxStep);
        if (number.ToString().Length % 2 != 0) return RecursiveStep(number * 2024, step + 1, maxStep);
        string s = number.ToString();
        return RecursiveStep(long.Parse(s[..(s.Length / 2)]), step + 1, maxStep) + RecursiveStep(long.Parse(s[(s.Length / 2)..]), step + 1, maxStep);
    }
}