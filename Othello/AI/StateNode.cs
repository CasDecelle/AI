using Othello.AI.Util;
using Othello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    public class StateNode : Node
    {
        private readonly AIBoard board;
        private readonly Player maxPlayer;
        private NodeType type;
        private Tuple<int, int> move;
        private double heuristicValue;

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
            get { return this.move; }
            set { this.move = value; }
        }

        public StateNode(AIBoard b, Player p, NodeType t)
        {
            this.board = b;
            this.maxPlayer = p;
            this.type = t;
        }

        public StateNode(AIBoard b, Player p, NodeType t, Tuple<int, int> m)
        {
            this.board = b;
            this.maxPlayer = p;
            this.type = t;
            this.move = m;
        }

        public void CalculateHeuristicValue()
        {
            if(this.IsLeaf) {
                heuristicValue = board.GetHeuristicValue(maxPlayer.Color);
                return;
            }
            switch (this.NodeType)
                {
                    case NodeType.MAX_NODE:
                        this.HeuristicValue = this.Children.Max(node => ((StateNode) node).HeuristicValue);
                        break;
                    case NodeType.MIN_NODE:
                        this.HeuristicValue = this.Children.Min(node => ((StateNode)node).HeuristicValue);
                        break;
                }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("| ");
            sb.Append(this.type.ToString());
            sb.Append(" | MOVE: ");
            if (move != null)
            {
                sb.Append(this.move.Item1);
                sb.Append(", ");
                sb.Append(this.move.Item2);
            }
            else
            {
                sb.Append("NO VALID MOVES!");
            }
            sb.Append(" | HV: ");
            if (this.HeuristicValue == Double.PositiveInfinity)
                sb.Append("+\u221E");
            else if (this.HeuristicValue == Double.NegativeInfinity)
                sb.Append("-\u221E");
            else
            {
                sb.Append(this.HeuristicValue);
            }
            sb.Append("\n");
            return sb.ToString();
        }
    }
}
