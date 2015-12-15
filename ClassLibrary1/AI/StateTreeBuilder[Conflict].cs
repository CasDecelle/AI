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
    public class StateTreeBuilder
    {
        private Tree<State> stateTree;

        public StateTreeBuilder(Board b, IHeuristic h, DiscColor dc)
        {
            ArrayList ar = b.GetValidMovesForPlayer(dc);
            Node<State> rootState = new Node<State>(new State(b, h));

            foreach (object o in ar) //Dit construeert het niveau na de root, moet recursief!
            {
                Node<State> nextState = new Node<State>(new State(b, h));
                Tuple<int, int> move = (Tuple<int, int>) o;       
                nextState.Data.Board.MakeMove(move.Item1, move.Item2, dc, nextState.Data.Board.IsMoveValid(move.Item1, move.Item2, dc));
                //DIT HIERBOVEN MOETEN WE REFACTOREN. Makemove moet ervan uitgaan dat de move al valid is en gewoon voor alle richtingen checken.
                rootState.AddChild(nextState);
            }
            stateTree = new Tree<State>(rootState);

        }


    }
}
