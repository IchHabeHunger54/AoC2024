using System.Collections.Concurrent;

namespace AoC2024;

public class Day11() : Day(11)
{
    private static readonly ConcurrentDictionary<(long value, int step), long> Cache = new();

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
            int j = i;
            /*Thread thread = new(() => */results.Add(RecursiveStep(numbers[j], runs))/*);
            threads.Add(thread);
            thread.Start()*/;
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        return results.Sum();
    }

    private static long RecursiveStep(long number, int step)
    {
        return step == 0
            ? 1
            : Cache.GetOrAdd((number, step), n =>
            {
                if (number == 0) return RecursiveStep(1, step - 1);
                string s = n.value.ToString();
                if (s.Length % 2 != 0) return RecursiveStep(n.value * 2024, step - 1);
                return RecursiveStep(long.Parse(s[..(s.Length / 2)]), step - 1) + RecursiveStep(long.Parse(s[(s.Length / 2)..]), step - 1);
            });
    }
}