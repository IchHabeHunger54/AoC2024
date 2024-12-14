using AoC2024.util;

namespace AoC2024;

public class Day14() : Day(14)
{
    protected override long TaskOne(List<string> lines)
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
        int runs = 100;
        int sizeX = 101;
        int sizeY = 103;
        int halfSizeX = sizeX / 2;
        int halfSizeY = sizeY / 2;
        for (int i = 0; i < runs; i++)
        {
            for (int j = 0; j < robots.Count; j++)
            {
                int x = (robots[j].x + velocities[j].x + sizeX) % sizeX;
                int y = (robots[j].y + velocities[j].y + sizeY) % sizeY;
                robots[j] = new Position(x, y);
            }
        }
        long q1 = 0, q2 = 0, q3 = 0, q4 = 0;
        foreach (Position robot in robots)
        {
            long x = robot.x;
            long y = robot.y;
            if (x < halfSizeX && y < halfSizeY)
            {
                q1++;
            }
            if (x > halfSizeX && y < halfSizeY)
            {
                q2++;
            }
            if (x < halfSizeX && y > halfSizeY)
            {
                q3++;
            }
            if (x > halfSizeX && y > halfSizeY)
            {
                q4++;
            }
        }
        return q1 * q2 * q3 * q4;
    }

    protected override long TaskTwo(List<string> lines)
    {
        throw new NotImplementedException();
    }
}