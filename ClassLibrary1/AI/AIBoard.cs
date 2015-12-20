using Othello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    public class AIBoard : Board
    {
        private Player maxPlayer;
        private NodeType nodeType;
        private HeuristicValueDeterminator heuristicValueDeterminator;
        private int maxDepth;

      /*  public IHeuristic Heuristic 
        {
            get { return this.heuristic;  }
        }*/

        public AIBoard(Board b, /*HeuristicValueDeterminator h,*/ Player p, NodeType t, int d) : base(b)
        {
            this.maxPlayer = p;
            this.nodeType = t;
           // this.heuristic = h;
            this.heuristicValueDeterminator = new SimpleHeuristicValueDeterminator();
            this.maxDepth = d;
        }

        public double GetHeuristicValue(DiscColor dc){
            return heuristicValueDeterminator.CalculateHeuristicValue(this, dc);
        }
    }
}
