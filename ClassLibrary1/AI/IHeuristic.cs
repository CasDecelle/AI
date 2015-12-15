using Othello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    public interface IHeuristic
    {
        double CalculateHeuristicValue(HeuristicBoard board, DiscColor color);
    }
}
