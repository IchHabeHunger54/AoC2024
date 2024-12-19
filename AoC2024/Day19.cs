namespace AoC2024;

public class Day19() : Day(19)
{
    protected override long TaskOne(List<string> lines)
    {
        List<string> towels = lines[0].Split(", ").ToList();
        Dictionary<string, bool> cache = new() { [""] = true };
        return lines[2..].Select(e => HasPattern(e, towels, cache) ? 1 : 0).Sum();
    }

    private static bool HasPattern(string pattern, List<string> towels, Dictionary<string, bool> cache)
    {
        if (cache.ContainsKey(pattern)) return cache[pattern];
        List<string> list = towels.Where(pattern.StartsWith).ToList();
        if (list.Any(e => HasPattern(pattern[e.Length..], towels, cache)))
        {
            cache[pattern] = true;
            return true;
        }
        cache[pattern] = false;
        return false;
    }

    protected override long TaskTwo(List<string> lines)
    {
        throw new NotImplementedException();
    }
}