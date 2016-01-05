using Othello.AI.Util;
using Othello.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    public class State
    {
        private int maxDepth;
        private StateNode maxNode;
        private readonly Player maxPlayer;
        private List<StateNode> tree;

        public State(Board b, int d, Player p, Tuple<int, int> lastMove)
        {
            this.maxDepth = d;
            this.maxPlayer = p;
            this.BuildState(b);
            this.maxNode.Move = lastMove;
            this.tree = new List<StateNode>();
            //this.maxNode.CalculateHeuristic();
        }

        public State(State previousState)
        {
            //board = previousState.board;
        }

        private void BuildState(Board b)
        {
            this.maxNode = new StateNode(new AIBoard(b, this.maxPlayer, NodeType.MAX_NODE, this.maxDepth), this.maxPlayer, NodeType.MAX_NODE);
            this.AddChildren(this.maxNode, this.maxDepth, this.maxPlayer, this.maxPlayer.Color);
        }

        private void AddChildren(StateNode parentNode, int depth, Player player, DiscColor color)
        {
            if (depth == 0) return;

            ArrayList validMoves = new ArrayList();
            validMoves = parentNode.Board.GetValidMovesForPlayer(color);

            if (validMoves != null)
            {
                foreach (Tuple<int, int> move in validMoves)
                {
                    if (move != null)
                    {
                        //Make new AIBoard based on the original board
                        AIBoard b = new AIBoard(parentNode.Board, this.maxPlayer, NodeTypeExtensions.GetOppositeType(parentNode.NodeType), depth);

                        //Execute Move
                        ArrayList flankingDirections = new ArrayList();
                        flankingDirections = b.IsMoveValid(move.Item1, move.Item2, color);
                        b.MakeMove(move.Item1, move.Item2, color, flankingDirections);

                        //Construct StateNode
                        StateNode node = new StateNode(b, player, NodeTypeExtensions.GetOppositeType(parentNode.NodeType), move, color);
                        parentNode.AddChild(node);

                        //New DiscColor
                        Disc invertedDisc = new Disc(color);
                        invertedDisc.InvertDisc();

                        //Recursive call for new state
                        this.AddChildren(node, depth - 1, player, invertedDisc.Color);

                        node.CalculateHeuristicValue();
                    }
                }
            }
            
        }

        public StateNode GetBestMove()
        {
            return (StateNode) maxNode.Children.First(mn => ((StateNode)mn).HeuristicValue == maxNode.Children.Max(node => ((StateNode)node).HeuristicValue));
        }

        public void PrintTree()
        {
            string s = Node.Print(this.maxNode, "", 0);
            File.WriteAllText("../../../Othello/Resources/Tree.txt", s);
        }
    }
}
