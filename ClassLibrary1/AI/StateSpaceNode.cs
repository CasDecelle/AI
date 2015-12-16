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
        private Tuple<int, int> move;

        public HeuristicBoard HeuristicBoard
        {
            get { return this.board; }
        }

        public NodeType NodeType 
        {
            get { return this.type; } 
        }

        public Tuple<int, int> Move 
        { 
            get { return this.move; } }

        public StateSpaceNode(HeuristicBoard b, Player p, NodeType t)
        {
            this.board = b;
            this.maxPlayer = p;
            this.type = t;
        }

        public StateSpaceNode(HeuristicBoard b, Player p, NodeType t, Tuple<int, int> m)
        {
            this.board = b;
            this.maxPlayer = p;
            this.type = t;
            this.move = m;
        }

        public double CalculateHeuristic()
        {
            return this.board.Heuristic.CalculateHeuristicValue(this.board, this.maxPlayer.Color);
        }



    }
}
