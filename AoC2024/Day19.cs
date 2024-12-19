namespace AoC2024;

public class Day19() : Day(19)
{
    protected override long TaskOne(List<string> lines)
    {
        List<string> towels = lines[0].Split(", ").ToList();
        Dictionary<string, long> cache = new() { [""] = 1 };
        return lines[2..].Select(e => Math.Clamp(HasPattern(e, towels, cache), 0, 1)).Sum();
    }

    private static long HasPattern(string pattern, List<string> towels, Dictionary<string, long> cache)
    {
        if (cache.ContainsKey(pattern)) return cache[pattern];
        List<string> list = towels.Where(pattern.StartsWith).ToList();
        cache[pattern] = list.Select(e => HasPattern(pattern[e.Length..], towels, cache)).Sum();
        return cache[pattern];
    }

    protected override long TaskTwo(List<string> lines)
    {
        List<string> towels = lines[0].Split(", ").ToList();
        Dictionary<string, long> cache = new() { [""] = 1 };
        return lines[2..].Select(e => HasPattern(e, towels, cache)).Sum();
    }
}