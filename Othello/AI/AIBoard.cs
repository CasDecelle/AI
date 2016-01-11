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

        public Player MaxPlayer
        {
            get { return this.maxPlayer; }
            set { this.maxPlayer = value; }
        }

        public double GetHeuristicValue(){
            return heuristicValueDeterminator.CalculateHeuristicValue(this);
        }

        private HeuristicValueDeterminator SetHeuristicValueDeterminator(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.EASY:
                    {
                        return new EasyHeuristicValueDeterminator();
                    }
                case Difficulty.MEDIUM:
                    {
                        return new MediumHeuristicValueDeterminator();
                    }
                case Difficulty.HARD:
                    {
                        return new HardHeuristicValueDeterminator();
                    }
            }
            return new EasyHeuristicValueDeterminator();
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

        public int GetEdges(DiscColor dc)
        {
            int edges = 0;
            for (int i = 0; i < 8; i++)
            {
                if (this.GetDiskFromSquare(i, 0) != null && this.GetDiskFromSquare(i, 0).Color == dc) edges++;
                if (this.GetDiskFromSquare(i, 7) != null && this.GetDiskFromSquare(i, 7).Color == dc) edges++;
                if (this.GetDiskFromSquare(0, i) != null && this.GetDiskFromSquare(0, i).Color == dc) edges++;
                if (this.GetDiskFromSquare(7, i) != null && this.GetDiskFromSquare(7, i).Color == dc) edges++;
            }
            return edges;
        }

        
    }
}
