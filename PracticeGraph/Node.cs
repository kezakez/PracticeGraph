using System.Collections.Generic;

namespace PracticeGraph
{
    public class Node
    {
        public Node(string name)
        {
            Name = name;
            EdgeDistances = new Dictionary<Node, int>();
        }

        public string Name { get; }

        public Dictionary<Node, int> EdgeDistances { get; }

        public void AddEdge(Node node, int distance)
        {
            EdgeDistances.Add(node, distance);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}