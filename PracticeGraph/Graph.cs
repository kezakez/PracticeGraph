using System.Collections.Generic;
using System.Linq;

namespace PracticeGraph
{
    public static class Graph
    {
        public static int GetDistance(params Node[] path)
        {
            var sum = 0;
            for (var index = 0; index < path.Length - 1; index++)
            {
                var node = path[index];
                var nodeNext = path[index + 1];
                if (node.EdgeDistances.ContainsKey(nodeNext))
                {
                    sum += node.EdgeDistances[nodeNext];
                }
                else
                {
                    return 0;
                }
            }
            return sum;
        }

        private class PathNode
        {
            public PathNode ParentNode { get; set; }
            public Node Value { get; set; }
            public Node[] ToArray()
            {
                var result = new LinkedList<Node>();
                var current = this;
                while (current != null)
                {
                    result.AddFirst(current.Value);
                    current = current.ParentNode;
                }
                return result.ToArray();
            }

            public int Count()
            {
                return ToArray().Length;
            }
        }

        public static List<Node[]> GetPathsWithMaximumHops(Node startNode, Node stopNode, int maximumHops)
        {
            var result = new List<Node[]>();
            var searchQueue = new Queue<PathNode>();
            searchQueue.Enqueue(new PathNode { Value = startNode });
            do
            {
                var currentNode = searchQueue.Dequeue();

                if (currentNode.Count() > maximumHops)
                {
                    break;
                }

                foreach (var nextNode in currentNode.Value.EdgeDistances.Keys)
                {
                    var currentPath = new PathNode { ParentNode = currentNode, Value = nextNode };
                    searchQueue.Enqueue(currentPath);

                    if (nextNode == stopNode)
                    {
                        result.Add(currentPath.ToArray());
                    }
                }
            } while (searchQueue.Count > 0);

            return result;
        }

        public static List<Node[]> GetPathsWithHops(Node startNode, Node stopNode, int hops)
        {
            var result = GetPathsWithMaximumHops(startNode, stopNode, hops);
            return result.Where(path => path.Length - 1 == hops).ToList();
        }

        public static int GetShortestPathDistance(Node startNode, Node stopNode)
        {
            throw new System.NotImplementedException();
        }

        public static List<Node[]> GetPathsWithMaximumDistance(Node startNode, Node stopNode, int maxDistance)
        {
            throw new System.NotImplementedException();
        }
    }
}