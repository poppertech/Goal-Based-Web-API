using Api.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Models.Network
{
    public class Network
    {
        private readonly INodeSimulator _nodeSimulator;
        private readonly Stack<Node> _stack;

        public Network(INodeSimulator nodeSimulator)
        {
            _nodeSimulator = nodeSimulator;
            _stack = new Stack<Node>();
        }

        public Node Tree { get; set; }

        public Portfolio Portfolio { get; private set; }

        public void Calculate(Node tree, IList<CashFlow> cashFlows)
        {
            var nodes = Traverse(tree);
            IDictionary<int, Node> nodeDictionary = nodes.ToDictionary(n => n.Id, n => n);
            Calculate(ref nodeDictionary, cashFlows);
        }

        public void Calculate(ref IDictionary<int, Node> nodeDictionary, IList<CashFlow> cashFlows)
        {
            _nodeSimulator.SimulateNodes(ref nodeDictionary);
            IList<Node> nodes = nodeDictionary.Values.ToList();
            Portfolio = new Portfolio(ref nodes, cashFlows);
            Tree = GenerateTree(nodes);
        }

        private Node GenerateTree(IList<Node> nodes)
        {
            var childHash = nodes.ToLookup(n => n.Parent.Id);
            foreach (var node in nodes)
            {
                node.Children = childHash[node.Id];
            }
            return nodes[0];
        }

        private IEnumerable<Node> Traverse(Node root)
        {
            Func<Node, IEnumerable<Node>> children = node => node.Children;
            _stack.Clear();
            _stack.Push(root);
            while (_stack.Count != 0)
            {
                Node item = _stack.Pop();
                yield return item;
                foreach (var child in item.Children)
                {
                    child.Parent = item;
                    _stack.Push(child);
                }

            }
        }

    }
}
