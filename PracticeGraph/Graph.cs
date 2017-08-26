using System.Collections.Generic;

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
            throw new System.NotImplementedException();
        }

        public static List<Node[]> GetPathsWithHops(Node startNode, Node stopNode, int hops)
        {
            throw new System.NotImplementedException();
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