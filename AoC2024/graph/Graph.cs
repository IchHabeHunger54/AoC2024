namespace AoC2024.graph;

public class Graph<T>
{
    public List<Node<T>> Nodes { get; } = [];
    public HashSet<Edge<T>> Edges { get; } = [];

    public Node<T> AddNode(T value)
    {
        Node<T> node = new(value);
        Nodes.Add(node);
        return node;
    }

    public Node<T> GetNode(T value)
    {
        return Nodes.First(n => n.Value!.Equals(value));
    }

    public void RemoveNode(T value)
    {
        RemoveNode(GetNode(value));
    }

    public void RemoveNode(Node<T> value)
    {
        Nodes.RemoveAll(n => n.Value!.Equals(value));
    }

    public Edge<T> AddEdge(Node<T> from, Node<T> to, int weight = 1, bool oneWay = false)
    {
        Edge<T> edge = new(from, to, weight, oneWay);
        Edges.Add(edge);
        return edge;
    }

    public void RemoveEdge(Node<T> from, Node<T> to)
    {
        Edges.RemoveWhere(e => e.From == from && e.To == to || !e.OneWay && e.From == to && e.To == from);
    }

    public List<Edge<T>> GetAdjacentEdges(Node<T> node)
    {
        return Edges.Where(e => e.To == node || !e.OneWay && e.From == node).ToList();
    }

    public List<Node<T>> GetAdjacentNodes(Node<T> node)
    {
        return GetAdjacentEdges(node).Select(e => e.From == node ? e.To : e.From).ToList();
    }

    public Edge<T>? GetEdge(Node<T> from, Node<T> to)
    {
        return Edges.FirstOrDefault(e => e.From == from && e.To == to || !e.OneWay && e.From == to && e.To == from);
    }

    public bool HasEdge(Node<T> from, Node<T> to)
    {
        return GetEdge(from, to) != null;
    }

    public void RemoveNodeAndEdges(Node<T> node)
    {
        RemoveNode(node);
        foreach (Edge<T> edge in GetAdjacentEdges(node))
        {
            Edges.Remove(edge);
        }
    }

    public static Graph<T> operator +(Graph<T> graph, T value)
    {
        graph.AddNode(value);
        return graph;
    }
}