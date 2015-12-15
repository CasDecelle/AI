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
        private Player maxPlayer;
        private NodeType nodeType;
        private IHeuristic heuristic;
        private int maxDepth;

        public HeuristicBoard(Board b, IHeuristic h, Player p, NodeType t, int d) : base(b)
        {
            this.maxPlayer = p;
            this.nodeType = t;
            this.heuristic = h;
            this.maxDepth = d;
        }
    }
}
