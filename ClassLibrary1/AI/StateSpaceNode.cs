using Othello.AI.Util;
using Othello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    public class StateSpaceNode : Node
    {
        private readonly AIBoard board;
        private readonly Player maxPlayer;
        private NodeType type;
        private Tuple<int, int> move;
        private DiscColor currentColor;
        private double heuristicValue;

        public DiscColor CurrentColor
        {
            get { return currentColor; }
            set { currentColor = value; }
        }

        public AIBoard Board
        {
            get { return this.board; }
        }

        public NodeType NodeType 
        {
            get { return this.type; } 
        }

        public double HeuristicValue
        {
            set { heuristicValue = value; }
            get { return this.heuristicValue;  }
        }

        public Tuple<int, int> Move 
        { 
            get { return this.move; } }

        public StateSpaceNode(AIBoard b, Player p, NodeType t)
        {
            this.board = b;
            this.maxPlayer = p;
            this.type = t;
        }

        public StateSpaceNode(AIBoard b, Player p, NodeType t, Tuple<int, int> m, DiscColor dc)
        {
            this.board = b;
            this.maxPlayer = p;
            this.type = t;
            this.move = m;
            this.currentColor = dc;
        }

        public void CalculateHeuristicValue()
        {
            if(this.IsLeaf) {
                heuristicValue = board.GetHeuristicValue(currentColor);
                return;
            }
            switch (this.NodeType)
                {
                    case NodeType.MAX_NODE:
                        this.HeuristicValue = this.Children.Max(node => ((StateSpaceNode) node).HeuristicValue);
                        break;
                    case NodeType.MIN_NODE:
                        this.HeuristicValue = this.Children.Min(node => ((StateSpaceNode)node).HeuristicValue);
                        break;
                }
            //return this.board.Heuristic.CalculateHeuristicValue(this.board, this.maxPlayer.Color);
        }



    }
}
