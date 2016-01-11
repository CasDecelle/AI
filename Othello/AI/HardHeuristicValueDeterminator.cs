using Othello.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    public class HardHeuristicValueDeterminator : HeuristicValueDeterminator
    {
        public double CalculateHeuristicValue(AIBoard b)
        {
            double heuristicValue = 0;

            DiscColor color = b.MaxPlayer.Color;
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
            int player_moves = 0;
            int opponent_moves = 0;
            if (b.GetValidMovesForPlayer(color) != null)
            {
                player_moves = b.GetValidMovesForPlayer(color).Count;
            }
            if (b.GetValidMovesForPlayer(opponentColor) != null)
            {
                opponent_moves = b.GetValidMovesForPlayer(opponentColor).Count;
            }
            if (player_moves > opponent_moves)
            {
                mobility = (100 * player_moves) / (player_moves + opponent_moves);
            }
            if (player_moves < opponent_moves)
            {
                mobility = -(100 * opponent_moves) / (player_moves + opponent_moves);
            }

            // Corner occupancy
            double corners = 25 * (b.GetCorners(color) - b.GetCorners(opponentColor));

            // Corner closeness
            double closeness = -12.5 * (b.GetCornerCloseness(color) - b.GetCornerCloseness(opponentColor));

            // Edges
            double edges = 12.5 * (b.GetEdges(color) - b.GetEdges(opponentColor));
            
            // Assign weights
            heuristicValue = (10 * discs) + (78.922 * mobility) + (801.724 * corners) + (382.026 * closeness) + (300 * edges);

            // Winning move
            if (b.IsGameFinished())
            {
                if (b.CountDiscs(color) < b.CountDiscs(opponentColor)) 
                    heuristicValue = Double.NegativeInfinity;
                if (b.CountDiscs(color) > b.CountDiscs(opponentColor))
                    heuristicValue = Double.PositiveInfinity;
            }

            return heuristicValue;
        }
    }
}
