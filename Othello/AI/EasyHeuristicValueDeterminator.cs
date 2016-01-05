using Othello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    public class EasyHeuristicValueDeterminator : HeuristicValueDeterminator 
    {
        public double CalculateHeuristicValue(AIBoard b, DiscColor color)
        {
            double heuristicValue = 0;

            Disc opponentDisc = new Disc(color);
            opponentDisc.InvertDisc();
            DiscColor opponentColor = opponentDisc.Color;

            // Disc count
            int player_discs = b.CountDiscs(color);
            int opponent_discs = b.CountDiscs(opponentColor);
            if (player_discs > opponent_discs)
            {
                heuristicValue = (100 * player_discs) / (player_discs + opponent_discs);
            }
            if (player_discs < opponent_discs)
            {
                heuristicValue = -(100 * opponent_discs) / (player_discs + opponent_discs);
            }

            // Winning move
            if (b.GetValidMovesForPlayer(color) != null && b.GetValidMovesForPlayer(color).Count == 0)
            {
                heuristicValue = Double.NegativeInfinity;
            }

            if (b.GetValidMovesForPlayer(opponentColor) != null && b.GetValidMovesForPlayer(opponentColor).Count == 0)
            {
                heuristicValue = Double.PositiveInfinity;
            }

            return heuristicValue;
        }
    }
}
