using Othello.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Model
{
    public class Robot : Player
    {
        private Board board;

        public Board Board
        {
            get { return this.board; }
            set { this.board = value; }
        }

        public Robot(Board board, String name, DiscColor color, Difficulty difficulty)
            : base(name, color)
        {
            this.board = board;
            this.Difficulty = difficulty;
        }

        public Tuple<int, int> GetBestMove(Tuple<int, int> lastMove)
        {
            State state = new State(board, 3, this, lastMove);
            state.PrintTree();
            StateNode bestNode = state.GetBestMove();
            return bestNode.Move;
        }        
    }
}
