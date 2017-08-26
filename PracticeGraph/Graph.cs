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

        public static List<Node[]> GetPathsWithMaximumHops(Node startNode, Node stopNode, int maximumHops)
        {
            var result = new List<Node[]>();
            var searchQueue = new Queue<PathNode>();
            searchQueue.Enqueue(new PathNode {Value = startNode});
            do
            {
                var currentNode = searchQueue.Dequeue();

                if (currentNode.Count() > maximumHops)
                {
                    break;
                }

                foreach (var nextNode in currentNode.Value.EdgeDistances.Keys)
                {
                    var currentPath = new PathNode {ParentNode = currentNode, Value = nextNode};
                    searchQueue.Enqueue(currentPath);

                    if (nextNode == stopNode)
                    {
                        result.Add(currentPath.ToArray());
                    }
                }
            } while (searchQueue.Any());

            return result;
        }

        public static List<Node[]> GetPathsWithHops(Node startNode, Node stopNode, int hops)
        {
            var result = GetPathsWithMaximumHops(startNode, stopNode, hops);
            return result.Where(path => path.Length - 1 == hops).ToList();
        }

        public static int? GetShortestPathDistance(Node startNode, Node stopNode)
        {
            var searchStack = new Stack<PathNode>();
            searchStack.Push(new PathNode {Value = startNode});
            int? shortestPathDistance = null;
            while (searchStack.Count > 0)
            {
                var currentNode = searchStack.Pop();

                if (currentNode.Value == stopNode && currentNode.Count() > 1)
                {
                    var distance = GetDistance(currentNode.ToArray());
                    if (distance < shortestPathDistance || !shortestPathDistance.HasValue)
                    {
                        shortestPathDistance = distance;
                    }
                }
                else
                {
                    if (!HasBeenVisited(searchStack, currentNode))
                    {
                        foreach (var nextNode in currentNode.Value.EdgeDistances.Keys)
                        {
                            var currentPath = new PathNode {ParentNode = currentNode, Value = nextNode};
                            searchStack.Push(currentPath);
                        }
                    }
                }
            }

            return shortestPathDistance;
        }

        private static bool HasBeenVisited(IEnumerable<PathNode> stack, PathNode node)
        {
            if (stack.Any(item => item.Value == node.Value))
            {
                return true;
            }

            var currentNode = node;
            while (currentNode.ParentNode != null)
            {
                currentNode = currentNode.ParentNode;

                if (node.Value == currentNode.Value)
                {
                    return true;
                }
            }

            return false;
        }

        public static List<Node[]> GetPathsWithMaximumDistance(Node startNode, Node stopNode, int maxDistance)
        {
            throw new System.NotImplementedException();
        }
    }
}