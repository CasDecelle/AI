using Othello.AI.Util;
using Othello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    public class StateTreeBuilder
    {
        private Tree<State> stateTree;

        public StateTreeBuilder(Board b, DiscColor dc)
        {
            //Node<StateNode>
        }

        private void AddChildren(State parentState, int depth, DiscColor color)
        {
            if (depth == 0) return;

            foreach (Tuple<int, int> move in parentState.Board.GetValidMovesForPlayer(color))
            {
                HeuristicBoard board = new HeuristicBoard(parentState.Board, new BasicHeuristics());
                board.MakeMove(move.Item1, move.Item2, color, board.IsMoveValid(move.Item1, move.Item2, color));
            }
        }
    }
}
