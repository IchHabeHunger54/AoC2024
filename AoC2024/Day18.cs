using AoC2024.graph;
using AoC2024.util;

namespace AoC2024;

public class Day18() : Day(18)
{
    protected override long TaskOne(List<string> lines)
    {
        const int size = 71;
        List<Position> positions = lines.Select(e => e.Split(",")).Select(e => new Position(int.Parse(e[0]), int.Parse(e[1]))).ToList()[..1024];
        Graph<Position> graph = new();
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                Node<Position> current = graph.AddNode(new Position(x, y));
                if (x > 0)
                {
                    graph.AddEdge(graph.GetNode(new Position(x - 1, y)), current);
                }
                if (y > 0)
                {
                    graph.AddEdge(graph.GetNode(new Position(x, y - 1)), current);
                }
            }
        }
        foreach (Position position in positions)
        {
            graph.RemoveNodeAndEdges(graph.GetNode(position));
        }
        return ShortestPath(graph, new Position(0, 0), new Position(size - 1, size - 1)).Count;
    }

    protected override long TaskTwo(List<string> lines)
    {
        throw new NotImplementedException();
    }

    private static List<Position> ShortestPath(Graph<Position> graph, Position from, Position to)
    {
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
            queue.Remove(u);
            foreach (Edge<Position> edge in graph.GetAdjacentEdges(u))
            {
                Node<Position> v = edge.GetOpposite(u);
                long alt = dist[u] + edge.Weight;
                if (alt >= dist[v]) continue;
                dist[v] = alt;
                prev[v] = u;
            }
        }
        List<Position> path = [];
        Node<Position> target = graph.GetNode(to);
        while (prev[target] != null)
        {
            path.Add(target.Value);
            if (target == source) break;
            target = prev[target]!;
        }
        return path;
    }
}