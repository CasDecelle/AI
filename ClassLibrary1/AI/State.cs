using Othello.AI.Util;
using Othello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    public class State
    {
        private HeuristicBoard board;
        private bool maxNode;
        private double heuristicValue;

        public HeuristicBoard Board
        {
            get { return this.board; }
            set { this.board = value; }
        }

        public State(Board b, IHeuristic h)
        {
            board = new HeuristicBoard(b, h);
        }

        public State(State previousState)
        {
            board = previousState.board;
        }
    }
}
