﻿using System.Collections.Generic;
using System.Linq;

namespace Api.Models.Network
{
    public class Portfolio
    {
        private readonly IList<Node> _nodes;
        private readonly IList<CashFlow> _cashFlows;

        private readonly int _numSimulations;

        public Portfolio(ref IList<Node> nodes, IList<CashFlow> cashFlows)
        {
            _cashFlows = cashFlows;
            _nodes = nodes.Where(n => n.IsPortfolioComponent).ToList();
            _numSimulations = _nodes[0].Simulations.Count / cashFlows.Count;

            InitialValue = _nodes.Sum(n => n.InitialInvestment.Value);
            SuccessProbabilities = new double[_cashFlows.Count];

            InitNodeProperties();
            CalculateSuccessCounts();
            CalculateSuccessProbabilities();
        }

        public double InitialValue { get; }

        public IList<double> SuccessProbabilities { get; }

        private void InitNodeProperties()
        {
            foreach (var node in _nodes)
            {
                InitializeNode(node);
            }
        }

        private void InitializeNode(Node node)
        {
            node.PortfolioWeight = node.InitialInvestment / InitialValue;
            node.ValueSimulations = new double[_numSimulations][];
            var cumulativeSimulations = new IList<double>[_numSimulations];
            for (var portfolioCnt = 0; portfolioCnt < _numSimulations; portfolioCnt++)
            {
                var investmentSimulations = new double[_cashFlows.Count];
                investmentSimulations[0] = node.InitialInvestment.Value;
                node.ValueSimulations[portfolioCnt] = investmentSimulations;

                var lBound = _cashFlows.Count * portfolioCnt;
                var uBound = _cashFlows.Count * (portfolioCnt + 1);
                var periodCumulativeSimulations = new double[_cashFlows.Count];
                for (int simulationCnt = lBound; simulationCnt < uBound; simulationCnt++)
                {
                    var index = simulationCnt % _cashFlows.Count;
                    var simulation = node.Simulations[simulationCnt].Price;
                    var cumulativeSimulation = simulation / node.InitialPrice;
                    periodCumulativeSimulations[index] = cumulativeSimulation.Value;
                }
                cumulativeSimulations[portfolioCnt] = periodCumulativeSimulations;
            }
            node.CumulativeSimulations = cumulativeSimulations;
        }

        private void CalculateSuccessCounts()
        {
            var portfolioSimulations = new double[_numSimulations][];
            for (int portfolioCnt = 0; portfolioCnt < _numSimulations; portfolioCnt++)
            {
                portfolioSimulations[portfolioCnt] = new double[_cashFlows.Count];
                for (int periodCnt = 0; periodCnt < _cashFlows.Count; periodCnt++)
                {
                    var portfolioValue = _nodes.Sum(n => n.ValueSimulations[portfolioCnt][periodCnt] * n.PortfolioWeight);
                    var portfolioValueNet = portfolioValue - _cashFlows[periodCnt].Cost;
                    portfolioValueNet = portfolioValueNet > 0 ? portfolioValueNet : 0;
                    portfolioSimulations[portfolioCnt][periodCnt] = portfolioValueNet.Value;
                    if (periodCnt < _cashFlows.Count - 1)
                        foreach (var node in _nodes)
                            node.ValueSimulations[portfolioCnt][periodCnt + 1] = node.CumulativeSimulations[portfolioCnt][periodCnt] * portfolioValueNet.Value * node.PortfolioWeight.Value;

                    SuccessProbabilities[periodCnt] = portfolioValueNet > 0 ? SuccessProbabilities[periodCnt] + 1 : SuccessProbabilities[periodCnt];
                }
            }
        }

        private void CalculateSuccessProbabilities()
        {
            for (int periodCnt = 0; periodCnt < _cashFlows.Count; periodCnt++)
            {
                SuccessProbabilities[periodCnt] = SuccessProbabilities[periodCnt] / _numSimulations;
            }
        }

    }
}
