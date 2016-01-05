using Othello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    public class MediumHeuristicValueDeterminator : HeuristicValueDeterminator
    {
        public double CalculateHeuristicValue(AIBoard b, Model.DiscColor color)
        {
            double heuristicValue = 0;

            Disc opponentDisc = new Disc(color); 
            opponentDisc.InvertDisc();
            DiscColor opponentColor = opponentDisc.Color;

            // Disc count
            double discs = 0;
            int player_discs = b.CountDiscs(color);
            int opponent_discs = b.CountDiscs(opponentColor);
            if (player_discs > opponent_discs)
            {
                discs = (100 * player_discs) / (player_discs + opponent_discs);
            }
            if (player_discs < opponent_discs)
            {
                discs = -(100 * opponent_discs) / (player_discs + opponent_discs);
            }

            // Mobility
            double mobility = 0;
            if (b.GetValidMovesForPlayer(color) != null && b.GetValidMovesForPlayer(opponentColor) != null)
            {
                int player_moves = b.GetValidMovesForPlayer(color).Count;
                int opponent_moves = b.GetValidMovesForPlayer(opponentColor).Count;
                if (player_moves > opponent_moves)
                {
                    mobility = (100 * player_moves) / (player_moves + opponent_moves);
                }
                if (player_moves < opponent_moves)
                {
                    mobility = -(100 * opponent_moves) / (player_moves + opponent_moves);
                }
            }
            
            // Assign weights
            heuristicValue = (10 * discs) + (78.922 * mobility);

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
