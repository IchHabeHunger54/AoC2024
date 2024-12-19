namespace AoC2024.graph;

public class Graph<T> where T : IComparable<T>
{
    public List<Node<T>> Nodes { get; } = [];
    public Dictionary<Node<T>, List<Edge<T>>> Neighbors { get; } = [];

    public Node<T> AddNode(T value)
    {
        if (ContainsNode(value)) return GetNode(value);
        Node<T> node = new(value);
        Nodes.Add(node);
        return node;
    }

    public bool ContainsNode(T value)
    {
        return Nodes.Any(e => e.Value.Equals(value));
    }

    public Node<T> GetNode(T value)
    {
        return Nodes.First(n => n.Value.Equals(value));
    }

    public void RemoveNode(T value)
    {
        Nodes.RemoveAll(e => e.Value.Equals(value));
    }

    public Edge<T> AddEdge(Node<T> from, Node<T> to, int weight = 1, bool oneWay = false)
    {
        Edge<T> edge = new(from, to, weight, oneWay);
        if (!Neighbors.TryGetValue(from, out List<Edge<T>>? value))
        {
            value = [];
            Neighbors.Add(from, value);
        }
        value.Add(edge);
        if (!Neighbors.TryGetValue(to, out value))
        {
            value = [];
            Neighbors.Add(to, value);
        }
        value.Add(edge);
        return edge;
    }

    public void RemoveEdge(Node<T> from, Node<T> to)
    {
        Edge<T>? edge = GetEdge(from, to);
        if (edge == null) return;
        Neighbors[from].Remove(edge);
        Neighbors[to].Remove(edge);
    }

    public List<Edge<T>> GetAdjacentEdges(Node<T> node)
    {
        return Neighbors.GetValueOrDefault(node, []);
    }

    public Edge<T>? GetEdge(Node<T> from, Node<T> to)
    {
        return Neighbors[from].FirstOrDefault(e => e.To == to || !e.OneWay && e.From == to);
    }

    public bool HasEdge(Node<T> from, Node<T> to)
    {
        return GetEdge(from, to) != null;
    }

    public void RemoveNodeAndEdges(Node<T> node)
    {
        RemoveNode(node.Value);
        foreach (Edge<T> edge in GetAdjacentEdges(node))
        {
            Neighbors[edge.GetOpposite(node)].Remove(edge);
        }
        Neighbors.Remove(node);
    }

    public Graph<T> GetConnectedSubgraph(T start)
    {
        DateTime startTime = DateTime.Now;
        Graph<T> subgraph = new();
        List<Node<T>> nodes = [GetNode(start)];
        HashSet<Node<T>> added = [nodes[0]];
        while (nodes.Count > 0)
        {
            Node<T> node = nodes[0];
            nodes.RemoveAt(0);
            Node<T> newNode = subgraph.AddNode(node.Value);
            foreach (Edge<T> edge in GetAdjacentEdges(node))
            {
                Node<T> opposite = edge.GetOpposite(node);
                if (!subgraph.ContainsNode(opposite.Value))
                {
                    if (added.Add(opposite))
                    {
                        nodes.Add(opposite);
                    }
                }
                else
                {
                    subgraph.AddEdge(newNode, subgraph.GetNode(opposite.Value), edge.Weight, edge.OneWay);
                }
            }
        }
        DateTime endTime = DateTime.Now;
        Console.WriteLine($"Benchmark: Generating subgraph took {(endTime - startTime).TotalMilliseconds} milliseconds");
        return subgraph;
    }
    
    public static Graph<T> operator +(Graph<T> graph, T value)
    {
        graph.AddNode(value);
        return graph;
    }
}