namespace AoC2024;

public class Day5() : Day(5)
{
    protected override long TaskOne(List<string> lines)
    {
        int i = 0;
        List<(int before, int after)> pages = [];
        List<List<int>> orders = [];
        for (string line = lines[0]; line != ""; i++, line = lines[i])
        {
            pages.Add(GetPage(line));
        }
        i++;
        for (; i < lines.Count; i++)
        {
            orders.Add(GetOrder(lines[i]));
        }
        return orders.Where(order => IsValid(pages, order)).Sum(order => order[order.Count / 2]);
    }

    protected override long TaskTwo(List<string> lines)
    {
        int i = 0;
        List<(int before, int after)> pages = [];
        List<List<int>> orders = [];
        for (string line = lines[0]; line != ""; i++, line = lines[i])
        {
            pages.Add(GetPage(line));
        }
        i++;
        for (; i < lines.Count; i++)
        {
            orders.Add(GetOrder(lines[i]));
        }
        int result = 0;
        foreach (List<int> order in orders.Where(order => !IsValid(pages, order)))
        {
            do
            {
                HashSet<int> processed = [];
                for (int j = 0; j < order.Count; j++)
                {
                    processed.Add(order[j]);
                    List<int> list = pages.Where(x => x.before == order[j]).Select(x => x.after).Intersect(processed).ToList();
                    if (list.Count == 0) continue;
                    order.Remove(list.First());
                    order.Insert(j, list.First());
                    break;
                }
            }
            while (!IsValid(pages, order));
            result += order[order.Count / 2];
        }
        return result;
    }

    private static (int before, int after) GetPage(string line)
    {
        int[] split = line.Split('|').Select(int.Parse).ToArray();
        return (before: split[0], after: split[1]);
    }

    private static List<int> GetOrder(string line)
    {
        return line.Split(',').Select(int.Parse).ToList();
    }

    private static bool IsValid(List<(int before, int after)> pages, List<int> order)
    {
        bool valid = true;
        HashSet<int> processed = [];
        foreach (int j in order)
        {
            processed.Add(j);
            if (pages.Where(x => x.before == j).Select(x => x.after).Intersect(processed).Any())
            {
                valid = false;
            }
        }
        return valid;
    }
}