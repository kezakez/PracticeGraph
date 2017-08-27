using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PracticeGraph.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var contents = "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7";
            if (args.Length == 1)
            {
                contents = GetFileContents(args[0]);
            }

            var result = (new GraphParser()).Parse(contents);
            if (result.Errors.Any())
            {
                WriteErrors(result.Errors);
            }
            else
            {
                WriteOutput(result.Nodes);
            }
            Console.ReadLine();
        }

        private static string GetFileContents(string filePath)
        {
            string contents;
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File specified on the command line '{filePath}' did not exist.");
            }
            using (var stream = File.OpenText(filePath))
            {
                contents = stream.ReadToEnd();
            }
            return contents;
        }

        private static void WriteErrors(IEnumerable<ParserError> resultErrors)
        {
            foreach (var error in resultErrors)
            {
                Console.WriteLine(error.Message);
            }
        }

        private static void WriteOutput(IReadOnlyDictionary<string, Node> nodes)
        {
            var defined = nodes.TryGetValue("A", out var a) & 
                         nodes.TryGetValue("B", out var b) &
                         nodes.TryGetValue("C", out var c) &
                         nodes.TryGetValue("D", out var d) &
                         nodes.TryGetValue("E", out var e);
            if (!defined)
            {
                Console.WriteLine($"Not all nodes defined A={a}, B={b}, C={c}, D={d}, E={e}");
                return;
            }

            Console.WriteLine($"Output #1: {FormatPathDistance(a, b, c)}");
            Console.WriteLine($"Output #2: {FormatPathDistance(a, d)}");
            Console.WriteLine($"Output #3: {FormatPathDistance(a, d, c)}");
            Console.WriteLine($"Output #4: {FormatPathDistance(a, e, b, c, d)}");
            Console.WriteLine($"Output #5: {FormatPathDistance(a, e, d)}");
            Console.WriteLine($"Output #6: {Graph.GetPathsWithMaximumHops(c, c, 3).Count}");
            Console.WriteLine($"Output #7: {Graph.GetPathsWithHops(a, c, 4).Count}");
            Console.WriteLine($"Output #8: {FormatShortestPathDistance(a, c)}");
            Console.WriteLine($"Output #9: {FormatShortestPathDistance(b, b)}");
            Console.WriteLine($"Output #10: {Graph.GetPathsWithMaximumDistance(c, c, 30).Count}");
        }

        private static string FormatPathDistance(params Node[] path)
        {
            var result = Graph.GetDistance(path);
            return result == 0 ? "NO SUCH ROUTE" : result.ToString();
        }

        private static string FormatShortestPathDistance(Node start, Node stop)
        {
            var result = Graph.GetShortestPathDistance(start, stop);
            return result.HasValue ? result.ToString() : "NO ROUTE FOUND";
        }
    }
}