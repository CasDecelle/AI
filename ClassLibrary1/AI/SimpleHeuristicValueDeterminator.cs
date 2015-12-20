using Othello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    public class SimpleHeuristicValueDeterminator : HeuristicValueDeterminator 
    {

        public double CalculateHeuristicValue(AIBoard b, DiscColor color)
        {
            return b.CountDiscs(color);
        }
    }
}
