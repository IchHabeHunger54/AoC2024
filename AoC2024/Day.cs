namespace AoC2024;

public abstract class Day(int dayNumber)
{
    public static void Main()
    {
        //RunAll();
        GetDay().Run();
    }

    private static Day GetDay()
    {
        return new Day12();
    }

    private static void RunAll()
    {
        Day[] days = [new Day1(), new Day2(), new Day3(), new Day4(), new Day5(), new Day6(), new Day7(), new Day8(), new Day9(), new Day10(), new Day11(), new Day12()];
        foreach (Day day in days)
        {
            day.Run();
        }
    }

    private static List<string> ReadLines(string path)
    {
        // We need to be relative to the bin/Debug/net9.0 directory to throw our files into the <project_root>/input directory, so here we go
        string absolutePath = AppDomain.CurrentDomain.BaseDirectory + "../../../../input/" + path;
        return File.ReadAllLines(absolutePath).ToList();
    }

    private void Run()
    {
        DateTime startTime = DateTime.Now;
        long task1 = TaskOne(ReadLines("day_" + dayNumber + "_task_1.txt"));
        DateTime endTime = DateTime.Now;
        Console.WriteLine($"Day {dayNumber}, Task 1: {task1} - took {(endTime - startTime).TotalMilliseconds} milliseconds");
        startTime = DateTime.Now;
        long task2 = TaskTwo(ReadLines("day_" + dayNumber + "_task_2.txt"));
        endTime = DateTime.Now;
        Console.WriteLine($"Day {dayNumber}, Task 2: {task2} - took {(endTime - startTime).TotalMilliseconds} milliseconds");
    }

    protected abstract long TaskOne(List<string> lines);

    protected abstract long TaskTwo(List<string> lines);
}