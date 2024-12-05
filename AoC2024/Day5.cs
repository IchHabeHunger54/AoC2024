namespace AoC2024;

public class Day5() : Day(5)
{
    protected override int TaskOne(List<string> lines)
    {
        int i = 0;
        List<(int before, int after)> pages = [];
        List<List<int>> orders = [];
        for (string line = lines[0]; line != ""; i++, line = lines[i])
        {
            int[] split = line.Split('|').Select(int.Parse).ToArray();
            pages.Add((before: split[0], after: split[1]));
        }
        i++;
        for (; i < lines.Count; i++)
        {
            orders.Add(lines[i].Split(',').Select(int.Parse).ToList());
        }
        int result = 0;
        foreach (List<int> order in orders)
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
            if (valid)
            {
                result += order[order.Count / 2];
            }
        }
        return result;
    }

    protected override int TaskTwo(List<string> lines)
    {
        throw new NotImplementedException();
    }
}