using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PracticeGraph
{
    public class GraphParser
    {
        public ParserResult Parse(string contents)
        {
            var tokenStore = new Dictionary<string, Node>();
            var edgeTokens = contents.Split(",");
            var errors = edgeTokens
                .Select(edgeToken => ParseEdge(tokenStore, edgeToken.Trim()))
                .Where(error => error != null)
                .ToList();

            return new ParserResult
            {
                Nodes = tokenStore,
                Errors = errors
            };
        }

        private static ParserError ValidateEdgeToken(string edgeToken)
        {
            if (edgeToken.Length < 3)
            {
                return new ParserError
                {
                    Error = ErrorType.InvalidEdgeFormat,
                    Message = $"Edge Token '{edgeToken}' has an invalid format. Must be in specified as 'AB1'"
                };
            }

            var fromToken = edgeToken[0].ToString();
            if (!IsValidNodeToken(fromToken))
            {
                return new ParserError
                {
                    Error = ErrorType.InvalidNodeToken,
                    Message = $"From Token '{fromToken}' in '{edgeToken}' is invalid. Must be a single character."
                };
            }

            var toToken = edgeToken[1].ToString();
            if (!IsValidNodeToken(toToken))
            {
                return new ParserError
                {
                    Error = ErrorType.InvalidNodeToken,
                    Message = $"To Token '{toToken}' in '{edgeToken}' is invalid. Must be a single character."
                };
            }

            return null;
        }

        private static bool IsValidNodeToken(string token)
        {
            return token.Length == 1 && Regex.IsMatch(token, "[a-zA-Z]");
        }

        private static ParserError ParseEdge(IDictionary<string, Node> tokenStore, string edgeToken)
        {
            var error = ValidateEdgeToken(edgeToken);
            if (error != null) return error;

            var edge = edgeToken.ToUpper().ToCharArray();

            var fromToken = edge[0];
            var toToken = edge[1];
            var distanceToken = string.Join("", edge.Skip(2));

            var fromNode = GetOrCreateNode(tokenStore, fromToken);
            var toNode = GetOrCreateNode(tokenStore, toToken);

            if (fromNode == toNode)
            {
                return new ParserError
                {
                    Error = ErrorType.InvalidEdgeReference,
                    Message = $"Edge '{edgeToken}' references itself"
                };
            }

            if (fromNode.EdgeDistances.ContainsKey(toNode))
            {
                return new ParserError
                {
                    Error = ErrorType.DuplicatePath,
                    Message = $"The specified edge '{edgeToken}' is a duplicate"
                };
            }

            if (int.TryParse(distanceToken, out var distance) && distance > 0)
            {
                fromNode.AddEdge(toNode, distance);
            }
            else
            {
                return new ParserError
                {
                    Error = ErrorType.InvalidDistance,
                    Message = $"Edge Token '{edgeToken}' had an invalid distance"
                };
            }
            return null;
        }


        private static Node GetOrCreateNode(IDictionary<string, Node> tokenStore, char token)
        {
            var tokenString = token.ToString();
            if (tokenStore.TryGetValue(tokenString, out var node)) return node;
            node = new Node(tokenString);
            tokenStore.Add(tokenString, node);
            return node;
        }
    }

    public enum ErrorType
    {
        InvalidDistance,
        InvalidEdgeFormat,
        InvalidNodeToken,
        InvalidEdgeReference,
        DuplicatePath
    }

    public class ParserError
    {

        public ErrorType Error { get; set; }
        public string Message { get; set; }
    }

    public class ParserResult
    {
        public Dictionary<string, Node> Nodes { get; set; }
        public List<ParserError> Errors { get; set; }
    }
}