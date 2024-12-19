using AoC2024.graph;
using AoC2024.util;

namespace AoC2024;

public class Day18() : Day(18)
{
    protected override long TaskOne(List<string> lines)
    {
        const int size = 71;
        const int initial = 1024;
        List<Position> positions = lines.Select(e => e.Split(",")).Select(e => new Position(int.Parse(e[0]), int.Parse(e[1]))).ToList()[..initial];
        Graph<Position> graph = GenerateInitialGraph(positions, size);
        return ShortestPath(graph, new Position(0, 0), new Position(size - 1, size - 1)).Count - 1;
    }

    protected override long TaskTwo(List<string> lines)
    {
        const int size = 71;
        const int initial = 1024;
        Position start = new(0, 0);
        Position end = new(size - 1, size - 1);
        List<Position> positions = lines.Select(e => e.Split(",")).Select(e => new Position(int.Parse(e[0]), int.Parse(e[1]))).ToList();
        Graph<Position> graph = GenerateInitialGraph(positions[..initial], size).GetConnectedSubgraph(start);
        List<Position> path = ShortestPath(graph, start, end);
        for (int i = initial; i < positions.Count; i++)
        {
            Position position = positions[i];
            if (!graph.ContainsNode(position)) continue;
            graph.RemoveNodeAndEdges(graph.GetNode(position));
            if (!path.Contains(position)) continue;
            Console.WriteLine($"Path has been blocked for list positon #{i}, recomputing graph and path");
            graph = graph.GetConnectedSubgraph(start);
            if (graph.ContainsNode(end))
            {
                path = ShortestPath(graph, start, end);
            }
            else
            {
                Console.WriteLine($"{position.x},{position.y}");
                return 0;
            }
        }
        return -1;
    }

    private static List<Position> ShortestPath(Graph<Position> graph, Position from, Position to)
    {
        DateTime startTime = DateTime.Now;
        Dictionary<Node<Position>, long> dist = new();
        Dictionary<Node<Position>, Node<Position>?> prev = new();
        List<Node<Position>> queue = [];
        foreach (Node<Position> node in graph.Nodes)
        {
            dist[node] = long.MaxValue;
            prev[node] = null;
            queue.Add(node);
        }
        Node<Position> source = graph.GetNode(from);
        dist[source] = 0;
        while (queue.Count > 0)
        {
            Node<Position> u = queue.OrderBy(n => dist[n]).First();
            foreach (Edge<Position> edge in graph.GetAdjacentEdges(u))
            {
                Node<Position> v = edge.GetOpposite(u);
                long alt = dist[u] + edge.Weight;
                if (alt >= dist[v]) continue;
                dist[v] = alt;
                prev[v] = u;
            }
            queue.Remove(u);
        }
        Node<Position> target = graph.GetNode(to);
        List<Position> path = [target.Value];
        while (prev[target] != null)
        {
            target = prev[target]!;
            path.Insert(0, target.Value);
        }
        DateTime endTime = DateTime.Now;
        Console.WriteLine($"Benchmark: Pathfinding took {(endTime - startTime).TotalMilliseconds} milliseconds");
        return path;
    }

    private static Graph<Position> GenerateInitialGraph(List<Position> positions, int size)
    {
        Graph<Position> graph = new();
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                Position position = new(x, y);
                if (positions.Contains(position)) continue;
                Node<Position> current = graph.AddNode(new Position(x, y));
                Position left = new(x - 1, y);
                Position up = new(x, y - 1);
                if (x > 0 && !positions.Contains(left))
                {
                    graph.AddEdge(graph.GetNode(left), current);
                }
                if (y > 0 && !positions.Contains(up))
                {
                    graph.AddEdge(graph.GetNode(up), current);
                }
            }
        }
        return graph;
    }
}