﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Api.Models.Network
{
    public class Node
    {
        public Node()
        {
            Distributions = new List<Distribution>();
        }

        public int Id { get; set; }

        public double? InitialPrice { get; set; }
        public double? InitialInvestment { get; set; }
        public double? PortfolioWeight { get; set; }
        public bool IsPortfolioComponent { get; set; }

        public IList<Distribution> Distributions { get; set; }
        [JsonIgnore]
        public IList<Simulation> Simulations { get; set; }
        [JsonIgnore]
        public IList<IList<double>> CumulativeSimulations { get; set; }
        [JsonIgnore]
        public IList<IList<double>> ValueSimulations { get; set; }

        public Node Parent { get; set; }

        public IEnumerable<Node> Children { get; set; }
    }
}
