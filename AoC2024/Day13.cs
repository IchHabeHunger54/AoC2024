namespace AoC2024;

public class Day13() : Day(13)
{
    protected override long TaskOne(List<string> lines)
    {
        long result = 0;
        for (int i = 0; i < lines.Count; i += 4)
        {
            (long xa, long ya, long xb, long yb, long xr, long yr) = ParseLinAlgSystem(lines[i], lines[i + 1], lines[i + 2]);
            long? l = SolveLinAlgSystem(xa, ya, xb, yb, xr, yr);
            if (l.HasValue)
            {
                result += l.Value;
            }
        }
        return result;
    }

    protected override long TaskTwo(List<string> lines)
    {
        long result = 0;
        for (int i = 0; i < lines.Count; i += 4)
        {
            (long xa, long ya, long xb, long yb, long xr, long yr) = ParseLinAlgSystem(lines[i], lines[i + 1], lines[i + 2]);
            long? l = SolveLinAlgSystem(xa, ya, xb, yb, xr + 10000000000000, yr + 10000000000000);
            if (l.HasValue)
            {
                result += l.Value;
            }
        }
        return result;
    }

    private static long? SolveLinAlgSystem(long xa, long ya, long xb, long yb, long xr, long yr)
    {
        double b = (xa * yr - xr * ya) / (double) (xa * yb - xb * ya);
        double a = (xr - b * xb) / xa;
        return !double.IsInteger(b) || !double.IsInteger(a) ? null : 3 * (long)a + (long)b;
    }
    
    private static (long xa, long ya, long xb, long yb, long xr, long yr) ParseLinAlgSystem(string equationA, string equationB, string result)
    {
        string[] split;
        // Equation A
        split = equationA.Split("X+")[1].Split(",");
        long xa = long.Parse(split[0]);
        long ya = long.Parse(split[1].Split("Y+")[1]);
        // Equation B
        split = equationB.Split("X+")[1].Split(",");
        long xb = long.Parse(split[0]);
        long yb = long.Parse(split[1].Split("Y+")[1]);
        // Result
        split = result.Split("X=")[1].Split(",");
        long xr = long.Parse(split[0]);
        long yr = long.Parse(split[1].Split("Y=")[1]);
        return (xa, ya, xb, yb, xr, yr);
    }
}