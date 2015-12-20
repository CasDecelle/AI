using Othello.AI;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public Robot(Board board, String name, DiscColor color)
            : base(name, color)
        {
            this.board = board;
        }

        public Tuple<int, int> GetMove()
        {
            StateSpace stateSpace = new StateSpace(board, 3, this);
            StateSpaceNode bestNode = stateSpace.GetBestMove();
            return bestNode.Move;
        }
            /* Tuple<int, int> move = null;
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
             * /

         /*public Tuple<int, int> GetMove() {
      
             ArrayList ar = board.GetValidMovesForPlayer(this.Color);

             //Zeer domme robot
             Tuple<int, int> returnTuple = (Tuple<int, int>)ar[1];
            
          

             return returnTuple;
         }*/

        
    }
}
