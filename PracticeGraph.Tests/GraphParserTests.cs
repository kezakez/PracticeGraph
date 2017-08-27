using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PracticeGraph.Tests
{
    [TestClass]
    public class GraphParserTests
    {
        [TestMethod]
        public void Parse()
        {
            var result = (new GraphParser()).Parse("AB555, bC4,CD8, DC8, de6, AD5,   CE2, EB3 , AE7");
            var nodes = result.Nodes;
            Assert.IsTrue(nodes.ContainsKey("A"));
            Assert.IsTrue(nodes.ContainsKey("B"));
            Assert.IsTrue(nodes.ContainsKey("C"));
            Assert.IsTrue(nodes.ContainsKey("D"));
            Assert.IsTrue(nodes.ContainsKey("E"));
            Assert.AreEqual(5, nodes.Count);

            Assert.AreEqual(555, nodes["A"].EdgeDistances[nodes["B"]]);
            Assert.AreEqual(4, nodes["B"].EdgeDistances[nodes["C"]]);
            Assert.AreEqual(8, nodes["C"].EdgeDistances[nodes["D"]]);
            Assert.AreEqual(8, nodes["D"].EdgeDistances[nodes["C"]]);
            Assert.AreEqual(6, nodes["D"].EdgeDistances[nodes["E"]]);
            Assert.AreEqual(5, nodes["A"].EdgeDistances[nodes["D"]]);
            Assert.AreEqual(2, nodes["C"].EdgeDistances[nodes["E"]]);
            Assert.AreEqual(3, nodes["E"].EdgeDistances[nodes["B"]]);
            Assert.AreEqual(7, nodes["A"].EdgeDistances[nodes["E"]]);
        }

        [TestMethod]
        public void ParseInvalidDistance()
        {
            Assert.IsTrue((new GraphParser()).Parse("BC4.1").Errors.Any(error => error.Error == ErrorType.InvalidDistance));

            Assert.IsTrue((new GraphParser()).Parse("BC-1").Errors.Any(error => error.Error == ErrorType.InvalidDistance));

            Assert.IsTrue((new GraphParser()).Parse("BCC").Errors.Any(error => error.Error == ErrorType.InvalidDistance));

            Assert.IsTrue((new GraphParser()).Parse("AB0").Errors.Any(error => error.Error == ErrorType.InvalidDistance));

            Assert.IsTrue((new GraphParser()).Parse("AB4 CD5").Errors.Any(error => error.Error == ErrorType.InvalidDistance));
        }

        [TestMethod]
        public void ParseDuplicatePath()
        {
            Assert.IsTrue((new GraphParser()).Parse("AB4, AB6").Errors.Any(error => error.Error == ErrorType.DuplicatePath));
        }

        [TestMethod]
        public void ParseInvalidNode()
        {
            Assert.IsTrue((new GraphParser()).Parse("444").Errors.Any(error => error.Error == ErrorType.InvalidNodeToken));

            Assert.IsTrue((new GraphParser()).Parse("A44").Errors.Any(error => error.Error == ErrorType.InvalidNodeToken));
        }

        [TestMethod]
        public void ParseInvalidEdgeFormat()
        {
            Assert.IsTrue((new GraphParser()).Parse("BC").Errors.Any(error => error.Error == ErrorType.InvalidEdgeFormat));

            Assert.IsTrue((new GraphParser()).Parse("AB5, , CD8").Errors.Any(error => error.Error == ErrorType.InvalidEdgeFormat));

            Assert.IsTrue((new GraphParser()).Parse("AB5, CD8, ").Errors.Any(error => error.Error == ErrorType.InvalidEdgeFormat));
        }

        [TestMethod]
        public void ParseInvalidEdgeReference()
        {
            Assert.IsTrue((new GraphParser()).Parse("AA5").Errors.Any(error => error.Error == ErrorType.InvalidEdgeReference));
        }
    }
}
