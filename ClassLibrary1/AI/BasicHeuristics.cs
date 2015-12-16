using Othello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    class BasicHeuristics : IHeuristic
    {
        public double CalculateHeuristicValue(HeuristicBoard board, DiscColor color)
        {
            double heuristicValue = board.CountDiscs(color);
            return heuristicValue;
        }
    }
}
