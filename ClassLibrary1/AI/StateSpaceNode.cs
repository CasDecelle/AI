using Othello.AI.Util;
using Othello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    class StateSpaceNode : Node
    {
        private readonly HeuristicBoard board;
        private readonly Player maxPlayer;
        private NodeType type;
        private double heuristicsValue;

        public HeuristicBoard HeuristicBoard
        {
            get { return this.board; }
        }
        public NodeType NodeType { get; set; }

        public StateSpaceNode(HeuristicBoard b, Player p, NodeType t)
        {
            this.board = b;
            this.maxPlayer = p;
            this.type = t;
        }

        public double CalculateHeuristic()
        {
            this.heuristicsValue = 0;
            return heuristicsValue;
        }



    }
}
