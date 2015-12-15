using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    public class BasicHeurstic : IHeuristic
    {
        public double CalculateHeuristicValue()
        {
            return 5.0;
        }
    }
}
