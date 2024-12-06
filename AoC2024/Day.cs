namespace AoC2024;

public abstract class Day(int dayNumber)
{
    public static void Main()
    {
        GetDay().Run();
    }

    private static Day GetDay()
    {
        return new Day6();
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
        int task1 = TaskOne(ReadLines("day_" + dayNumber + "_task_1.txt"));
        DateTime endTime = DateTime.Now;
        Console.WriteLine("Day " + dayNumber + ", Task 1: " + task1 + " - took " + (endTime - startTime).TotalMilliseconds + " milliseconds");
        startTime = DateTime.Now;
        int task2 = TaskTwo(ReadLines("day_" + dayNumber + "_task_2.txt"));
        endTime = DateTime.Now;
        Console.WriteLine("Day " + dayNumber + ", Task 2: " + task2 + " - took " + (endTime - startTime).TotalMilliseconds + " milliseconds");
    }

    protected abstract int TaskOne(List<string> lines);

    protected abstract int TaskTwo(List<string> lines);
}