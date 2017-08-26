using System.Collections.Generic;
using System.Linq;

namespace PracticeGraph
{
    internal class PathNode
    {
        public PathNode ParentNode { get; set; }
        public Node Value { get; set; }

        public Node[] ToArray()
        {
            return ToOrderedPath().ToArray();
        }

        private IEnumerable<Node> ToOrderedPath()
        {
            var result = new LinkedList<Node>();
            var current = this;
            while (current != null)
            {
                result.AddFirst(current.Value);
                current = current.ParentNode;
            }
            return result;
        }

        public int Count()
        {
            return ToArray().Length;
        }

        public override string ToString()
        {
            return string.Join(',', ToOrderedPath());
        }
    }
}