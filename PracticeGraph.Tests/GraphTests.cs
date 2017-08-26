using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PracticeGraph.Tests
{
    [TestClass]
    public class GraphTests
    {
        private Node _a;
        private Node _b;
        private Node _c;
        private Node _d;
        private Node _e;

        public void Setup()
        {
            _a = new Node("A");
            _b = new Node("B");
            _c = new Node("C");
            _d = new Node("D");
            _e = new Node("E");

            _a.AddEdge(_b, 5);
            _b.AddEdge(_c, 4);
            _c.AddEdge(_d, 8);
            _d.AddEdge(_c, 8);
            _d.AddEdge(_e, 6);
            _a.AddEdge(_d, 5);
            _c.AddEdge(_e, 2);
            _e.AddEdge(_b, 3);
            _a.AddEdge(_e, 7);
        }

        [TestMethod]
        public void GetDistance()
        {
            Setup();

            Assert.AreEqual(9, Graph.GetDistance(_a, _b, _c));
            Assert.AreEqual(5, Graph.GetDistance(_a, _d));
            Assert.AreEqual(13, Graph.GetDistance(_a, _d, _c));
            Assert.AreEqual(22, Graph.GetDistance(_a, _e, _b, _c, _d));
            Assert.AreEqual(0, Graph.GetDistance(_a, _e, _d));
        }

        [TestMethod]
        public void GetPathsWithMaximumHops()
        {
            Setup();

            var paths = Graph.GetPathsWithMaximumHops(_c, _c, 3);
            Assert.AreEqual(2, paths.Count);
        }

        [TestMethod]
        public void GetPathsWithHops()
        {
            Setup();

            var paths = Graph.GetPathsWithHops(_a, _c, 4);
            Assert.AreEqual(3, paths.Count);
        }

        [TestMethod]
        public void GetShortestPathDistance()
        {
            Setup();

            var shortestAtoCDistance = Graph.GetShortestPathDistance(_a, _c);
            Assert.AreEqual(9, shortestAtoCDistance);

            var shortestBtoBDistance = Graph.GetShortestPathDistance(_b, _b);
            Assert.AreEqual(9, shortestBtoBDistance);
        }

        [TestMethod]
        public void GetPathsWithMaximumDistance()
        {
            Setup();

            var pathsCtoC = Graph.GetPathsWithMaximumDistance(_c, _c, 30);
            Assert.AreEqual(7, pathsCtoC.Count);
        }
    }
}