using Othello.AI.Util;
using Othello.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    public class StateSpace
    {
        //private HeuristicBoard board;
        private int maxDepth;
        private StateSpaceNode maxNode;
        private readonly Player maxPlayer;

        /*
        public HeuristicBoard Board
        {
            get { return this.board; }
            set { this.board = value; }
        }*/

        public StateSpace(Board b, IHeuristic h, int d, Player p)
        {
            this.maxDepth = d;
            this.maxPlayer = p;
            this.BuildStateSpace(b, h);
            //this.maxNode.CalculateHeuristic();
        }

        public StateSpace(StateSpace previousState)
        {
            //board = previousState.board;
        }

        private void BuildStateSpace(Board b, IHeuristic h)
        {
            this.maxNode = new StateSpaceNode(new HeuristicBoard(b, h, this.maxPlayer, NodeType.MAX_NODE, this.maxDepth), this.maxPlayer, NodeType.MAX_NODE);
            this.AddChildren(this.maxNode, h, this.maxDepth, this.maxPlayer, this.maxPlayer.Color);
        }

        private void AddChildren(StateSpaceNode parentNode, IHeuristic heuristic, int depth, Player player, DiscColor color)
        {
            if (depth == 0) return;

            ArrayList validMoves = new ArrayList();
            validMoves = parentNode.HeuristicBoard.GetValidMovesForPlayer(color);

            foreach (Tuple<int, int> move in validMoves)
            {
                if (move != null)
                {
                    HeuristicBoard b = new HeuristicBoard(parentNode.HeuristicBoard, heuristic, this.maxPlayer, NodeTypeExtensions.GetOppositeType(parentNode.NodeType), depth);
                    ArrayList flankingDirections = new ArrayList();
                    flankingDirections = b.IsMoveValid(move.Item1, move.Item2, color);
                    b.MakeMove(move.Item1, move.Item2, color, flankingDirections);
                    StateSpaceNode node = new StateSpaceNode(b, player, NodeTypeExtensions.GetOppositeType(parentNode.NodeType), move);
                    parentNode.AddChild(node);
                    Disc invertedDisc = new Disc(color);
                    invertedDisc.InvertDisc();
                    this.AddChildren(node, heuristic, depth - 1, player, invertedDisc.Color);
                }
            }
        }

        public Tuple<int, int> GetBestMove()
        {
            Tuple<int, int> move = null;
            double heuristicValue = 0;
            // neemt enkel children van root voorlopig, TODO hele boom doorzoeken
            foreach (StateSpaceNode n in this.maxNode.Children)
            {
                if (n.CalculateHeuristic() > heuristicValue)
                {
                    heuristicValue = n.CalculateHeuristic();
                    move = n.Move;
                }
            }
            return move;
        }
    }
}
