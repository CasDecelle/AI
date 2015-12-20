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
        private int maxDepth;
        private StateSpaceNode maxNode;
        private readonly Player maxPlayer;
        private List<StateSpaceNode> tree;

        /*
        public HeuristicBoard Board
        {
            get { return this.board; }
            set { this.board = value; }
        }*/

        public StateSpace(Board b, int d, Player p)
        {
            this.maxDepth = d;
            this.maxPlayer = p;
            this.BuildStateSpace(b);
            this.tree = new List<StateSpaceNode>();
            //this.maxNode.CalculateHeuristic();
        }

        public StateSpace(StateSpace previousState)
        {
            //board = previousState.board;
        }

        private void BuildStateSpace(Board b)
        {
            this.maxNode = new StateSpaceNode(new AIBoard(b, this.maxPlayer, NodeType.MAX_NODE, this.maxDepth), this.maxPlayer, NodeType.MAX_NODE);
            this.AddChildren(this.maxNode, this.maxDepth, this.maxPlayer, this.maxPlayer.Color);
        }

        private void AddChildren(StateSpaceNode parentNode, int depth, Player player, DiscColor color)
        {
            if (depth == 0) return;

            ArrayList validMoves = new ArrayList();
            validMoves = parentNode.Board.GetValidMovesForPlayer(color);

            foreach (Tuple<int, int> move in validMoves)
            {
                if (validMoves != null && move != null)
                {
                    //Make new AIBoard based on the original board
                    AIBoard b = new AIBoard(parentNode.Board, this.maxPlayer, NodeTypeExtensions.GetOppositeType(parentNode.NodeType), depth);
                    
                    //Execute Move
                    ArrayList flankingDirections = new ArrayList();
                    flankingDirections = b.IsMoveValid(move.Item1, move.Item2, color);
                    b.MakeMove(move.Item1, move.Item2, color, flankingDirections);
                    
                    //Construct StateSpaceNode
                    StateSpaceNode node = new StateSpaceNode(b, player, NodeTypeExtensions.GetOppositeType(parentNode.NodeType), move, color);
                    //node.HeuristicValue = b.GetHeuristicValue(b, color);
                    parentNode.AddChild(node);
                   // tree.Add(node);

                    //New DiscColor
                    Disc invertedDisc = new Disc(color);
                    invertedDisc.InvertDisc();

                    //Recursive call for new state
                    this.AddChildren(node, depth - 1, player, invertedDisc.Color);

                    node.CalculateHeuristicValue();
                }
            }
        }

        public StateSpaceNode GetBestMove()
        {
          /*  Tuple<int, int> move = null;
            // neemt enkel children van root voorlopig, TODO hele boom doorzoeken
            List<StateSpaceNode> leafs = tree.Where(n => n.IsLeaf).ToList();
            foreach(StateSpaceNode l in leafs) {
                l.HeuristicValue = l.Board.GetHeuristicValue(l.CurrentColor);
            }
            List<StateSpaceNode> nodesToProcess = tree.Where(n => n.Children.Intersect(leafs).Any()).ToList();
           

            foreach (StateSpaceNode n in nodesToProcess)
            {
                switch (n.NodeType)
                {
                    case NodeType.MAX_NODE:
                        n.HeuristicValue = nodesToProcess.Max(np => np.HeuristicValue);
                        no
                        break;
                    case NodeType.MIN_NODE:
                        n.HeuristicValue = nodesToProcess.Min(np => np.HeuristicValue);
                        break;
                }
            }*/
            return (StateSpaceNode) maxNode.Children.First(mn => ((StateSpaceNode)mn).HeuristicValue == maxNode.Children.Max(node => ((StateSpaceNode)node).HeuristicValue));
        }
    }
}
