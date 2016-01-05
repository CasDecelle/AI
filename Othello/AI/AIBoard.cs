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

        public AIBoard(Board b, Player p, NodeType t, int i) : base(b)
        {
            this.maxPlayer = p;
            this.nodeType = t;
            this.maxDepth = i;
            this.heuristicValueDeterminator = this.SetHeuristicValueDeterminator(p.Difficulty);
            
        }

        public double GetHeuristicValue(DiscColor dc){
            return heuristicValueDeterminator.CalculateHeuristicValue(this, dc);
        }

        private HeuristicValueDeterminator SetHeuristicValueDeterminator(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.EASY:
                    {
                        return new SimpleHeuristicValueDeterminator();
                    }
                case Difficulty.MEDIUM:
                    {
                        return new SimpleHeuristicValueDeterminator();
                    }
                case Difficulty.HARD:
                    {
                        return new SimpleHeuristicValueDeterminator();
                    }
            }
            return new SimpleHeuristicValueDeterminator();
        }

        public int GetCorners(DiscColor dc)
        {
            int corners = 0;
            if (this.GetDiskFromSquare(0, 0) != null && this.GetDiskFromSquare(0, 0).Color == dc) corners++;
            if (this.GetDiskFromSquare(0, 7) != null && this.GetDiskFromSquare(0, 7).Color == dc) corners++;
            if (this.GetDiskFromSquare(7, 0) != null && this.GetDiskFromSquare(7, 0).Color == dc) corners++;
            if (this.GetDiskFromSquare(7, 7) != null && this.GetDiskFromSquare(7, 7).Color == dc) corners++;
            return corners;
        }

        public int GetCornerCloseness(DiscColor dc)
        {
            int cornerCloseness = 0;
            if (this.GetDiskFromSquare(0, 0) == null)
            {
                if (this.GetDiskFromSquare(0, 1) != null && this.GetDiskFromSquare(0, 1).Color == dc) cornerCloseness++;
                if (this.GetDiskFromSquare(1, 0) != null && this.GetDiskFromSquare(1, 0).Color == dc) cornerCloseness++;
                if (this.GetDiskFromSquare(1, 1) != null && this.GetDiskFromSquare(1, 1).Color == dc) cornerCloseness++;
            }
            if (this.GetDiskFromSquare(0, 7) == null)
            {
                if (this.GetDiskFromSquare(1, 7) != null && this.GetDiskFromSquare(1, 7).Color == dc) cornerCloseness++;
                if (this.GetDiskFromSquare(0, 6) != null && this.GetDiskFromSquare(0, 6).Color == dc) cornerCloseness++;
                if (this.GetDiskFromSquare(1, 6) != null && this.GetDiskFromSquare(1, 6).Color == dc) cornerCloseness++;
            }
            if (this.GetDiskFromSquare(7, 0) == null)
            {
                if (this.GetDiskFromSquare(7, 1) != null && this.GetDiskFromSquare(7, 1).Color == dc) cornerCloseness++;
                if (this.GetDiskFromSquare(6, 0) != null && this.GetDiskFromSquare(6, 0).Color == dc) cornerCloseness++;
                if (this.GetDiskFromSquare(6, 1) != null && this.GetDiskFromSquare(6, 1).Color == dc) cornerCloseness++;
            }
            if (this.GetDiskFromSquare(7, 7) == null)
            {
                if (this.GetDiskFromSquare(7, 6) != null && this.GetDiskFromSquare(7, 6).Color == dc) cornerCloseness++;
                if (this.GetDiskFromSquare(6, 7) != null && this.GetDiskFromSquare(6, 7).Color == dc) cornerCloseness++;
                if (this.GetDiskFromSquare(6, 6) != null && this.GetDiskFromSquare(6, 6).Color == dc) cornerCloseness++;
            }
            return cornerCloseness;
        }
    }
}
