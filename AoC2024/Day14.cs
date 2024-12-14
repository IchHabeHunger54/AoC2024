using AoC2024.util;

namespace AoC2024;

public class Day14() : Day(14)
{
    private const int SizeX = 101;
    private const int SizeY = 103;
    private const int HalfSizeX = SizeX / 2;
    private const int HalfSizeY = SizeY / 2;
    
    protected override long TaskOne(List<string> lines)
    {
        (List<Position> robots, List<Position> velocities) = ParseInput(lines);
        for (int i = 0; i < 100; i++)
        {
            UpdateRobots(robots, velocities);
        }
        return GetDangerLevel(robots);
    }

    // The tree appears by minimizing the danger level.
    // Credit: https://www.reddit.com/r/adventofcode/comments/1he0g67/2024_day_14_part_2_the_clue_was_in_part_1/
    protected override long TaskTwo(List<string> lines)
    {
        (List<Position> robots, List<Position> velocities) = ParseInput(lines);
        long minDangerLevel = long.MaxValue;
        long minIterations = 0;
        // 10k was enough for me, as the solution is around 7k.
        for (int i = 0; i < 10000; i++)
        {
            UpdateRobots(robots, velocities);
            long dangerLevel = GetDangerLevel(robots);
            if (dangerLevel >= minDangerLevel) continue;
            minDangerLevel = dangerLevel;
            minIterations = i + 1;
            Console.WriteLine($"Danger level: {dangerLevel}");
            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    Console.Write(robots.Count(r => r.x == x && r.y == y));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        return minIterations;
    }

    private static (List<Position> robots, List<Position> velocities) ParseInput(List<string> lines)
    {
        List<Position> robots = [];
        List<Position> velocities = [];
        foreach (string line in lines)
        {
            string[] split = line.Split(' ');
            int[] robot = split[0][2..].Split(',').Select(int.Parse).ToArray();
            int[] velocity = split[1][2..].Split(',').Select(int.Parse).ToArray();
            robots.Add(new Position(robot[0], robot[1]));
            velocities.Add(new Position(velocity[0], velocity[1]));
        }
        return (robots, velocities);
    }

    private static void UpdateRobots(List<Position> robots, List<Position> velocities)
    {
        for (int j = 0; j < robots.Count; j++)
        {
            int x = (robots[j].x + velocities[j].x + SizeX) % SizeX;
            int y = (robots[j].y + velocities[j].y + SizeY) % SizeY;
            robots[j] = new Position(x, y);
        }
    }

    private static long GetDangerLevel(List<Position> robots)
    {
        long q1 = 0, q2 = 0, q3 = 0, q4 = 0;
        foreach (Position robot in robots)
        {
            long x = robot.x;
            long y = robot.y;
            if (x < HalfSizeX && y < HalfSizeY)
            {
                q1++;
            }
            if (x > HalfSizeX && y < HalfSizeY)
            {
                q2++;
            }
            if (x < HalfSizeX && y > HalfSizeY)
            {
                q3++;
            }
            if (x > HalfSizeX && y > HalfSizeY)
            {
                q4++;
            }
        }
        return q1 * q2 * q3 * q4;
    }
}