using Othello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    public class HeuristicBoard : Board
    {
        private IHeuristic heuristic;

        public HeuristicBoard(Board b, IHeuristic h) : base(b)
        {
            heuristic = h;
        }
    }
}
