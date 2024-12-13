namespace AoC2024;

public class Day13() : Day(13)
{
    private const int MaxPresses = 100;
    
    protected override long TaskOne(List<string> lines)
    {
        long result = 0;
        for (int i = 0; i < lines.Count; i += 4)
        {
            // Button A
            string line = lines[i];
            string[] split = line.Split("X+")[1].Split(",");
            int xa = int.Parse(split[0]);
            int ya = int.Parse(split[1].Split("Y+")[1]);
            // Button B
            line = lines[i + 1];
            split = line.Split("X+")[1].Split(",");
            int xb = int.Parse(split[0]);
            int yb = int.Parse(split[1].Split("Y+")[1]);
            // Prize
            line = lines[i + 2];
            split = line.Split("X=")[1].Split(",");
            int xp = int.Parse(split[0]);
            int yp = int.Parse(split[1].Split("Y=")[1]);
            // If we cannot reach x/y by pressing both buttons 100 times each, skip.
            if (xa * MaxPresses + xb * MaxPresses < xp) continue;
            if (ya * MaxPresses + yb * MaxPresses < yp) continue;
            // Solve the linalg system.
            int bestScore = MaxPresses * 4 + 1;
            for (int j = 0; j < MaxPresses; j++)
            {
                // Try using only button A.
                int ax = xp - xa * j;
                int ay = yp - ya * j;
                if (ax < 0 || ay < 0) break;
                // If we can't get the rest using button B, skip.
                int bx = ax / xb;
                int by = ay / yb;
                if (ax % xb != 0 || bx >= MaxPresses || ay % yb != 0 || by >= MaxPresses || bx != by) continue;
                // If the current values are smaller than the previous best, set new best.
                int score = j * 3 + bx;
                if (score < bestScore)
                {
                    bestScore = score;
                }
            }
            if (bestScore < MaxPresses * 4)
            {
                result += bestScore;
            }
        }
        return result;
    }

    protected override long TaskTwo(List<string> lines)
    {
        throw new NotImplementedException();
    }
}