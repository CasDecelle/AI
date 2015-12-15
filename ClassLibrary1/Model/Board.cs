using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Model
{
    public class Board
    {
        private Square[,] boardSquares;
        public readonly int MAX_SQUARE_COUNT = 8;

        public Square[,] BoardSquares { 
            get { return this.boardSquares; } 
            set { this.boardSquares = value; } 
        }

        public Board()
        {
            this.boardSquares = new Square[this.MAX_SQUARE_COUNT, this.MAX_SQUARE_COUNT];
            this.initBoard();
        }

        public Board(Board b)
        {
            this.BoardSquares = new Square[this.MAX_SQUARE_COUNT, this.MAX_SQUARE_COUNT];
            for (int i = 0; i < this.MAX_SQUARE_COUNT; i++) {
                for (int j = 0; j < this.MAX_SQUARE_COUNT; j++)
                {
                    this.BoardSquares[i, j] = new Square(b.BoardSquares[i, j]);
                }
            }
        }

        public void initBoard()
        {
            for (int i = 0; i < this.MAX_SQUARE_COUNT; i++)
            {
                for (int j = 0; j < this.MAX_SQUARE_COUNT; j++)
                {
                    this.boardSquares[i,j] = new Square(i,j);
                }
            }
            this.boardSquares[3, 3].Disc = new Disc(DiscColor.White);
            this.boardSquares[3, 4].Disc = new Disc(DiscColor.Black);
            this.boardSquares[4, 3].Disc = new Disc(DiscColor.Black);
            this.boardSquares[4, 4].Disc = new Disc(DiscColor.White);
        }

        public Disc GetDiskFromSquare(int row, int col) { return this.boardSquares[row, col].Disc; }

        public void MakeMove(int row, int col, DiscColor color, ArrayList flankingDirections)
        {
            for (int i = 0; i < flankingDirections.Count; i++ )
            {
                int directionRow, directionCol;
                int tmpRow = row;
                int tmpCol = col;
                Tuple<int, int> directionTuple = (Tuple<int, int>)flankingDirections[i];
                directionRow = directionTuple.Item1;
                directionCol = directionTuple.Item2;

                this.boardSquares[tmpRow, tmpCol].Disc = new Disc(color);

                tmpRow += directionRow;
                tmpCol += directionCol;

                do
                {
                    InvertDisc(this.boardSquares[tmpRow, tmpCol].Disc);
                    tmpRow += directionRow;
                    tmpCol += directionCol;
                } while (this.boardSquares[tmpRow, tmpCol].Disc != null && this.boardSquares[tmpRow, tmpCol].Disc.Color != color);
            }
        }

        public void InvertDisc(Disc d)
        {
            d.Color =  d.Color == DiscColor.Black ? d.Color = DiscColor.White : DiscColor.Black;
        }

        public ArrayList IsMoveValid(int row, int col, DiscColor color)
        {
            ArrayList returnArray = new ArrayList();
            if (this.boardSquares[row, col].Disc != null)
                return null;

            int directionRow, directionCol;
            
            for (directionRow = -1; directionRow <= 1; directionRow++)
            {
                for (directionCol = -1; directionCol <= 1; directionCol++)
                {
                    if (MoveFlanksOpponentInDirection(row, col, color, directionRow, directionCol))
                    {
                        returnArray.Add(Tuple.Create(directionRow, directionCol));
                    }
                }
            }

            return returnArray = returnArray.Count > 0 ? returnArray : null;
        }

        public bool ValidMoveRemaining(DiscColor color)
        {
            for (int i = 0; i < this.MAX_SQUARE_COUNT; i++)
            {
                for (int j = 0; j < this.MAX_SQUARE_COUNT; j++)
                {
                    if (IsMoveValid(i, j, color) != null)
                        return true;
                }
            }
            return false;
        }

        public ArrayList GetValidMovesForPlayer(DiscColor color)
        {
            ArrayList returnList = new ArrayList();
            for (int i = 0; i < this.MAX_SQUARE_COUNT; i++)
            {
                for (int j = 0; j < this.MAX_SQUARE_COUNT; j++)
                {
                    if (IsMoveValid(i, j, color) != null)
                    {
                        returnList.Add(Tuple.Create(i, j));
                    }
                        
                }
            }
            return returnList = returnList.Count != 0 ? returnList : null;
        }

        public bool MoveFlanksOpponentInDirection(int originalRow, int originalCol, DiscColor color, int dRow, int dCol)
        {
            if (dRow == 0 && dCol == 0)
                return false;

            int row = originalRow + dRow;
            int col = originalCol + dCol;

            while (row < 8 && row >= 0 && col < 8 && col >= 0 && (this.boardSquares[row, col].Disc != null && this.boardSquares[row, col].Disc.Color != color))
            {
                row += dRow;
                col += dCol;
            }

            if (row < 0 || row > 7 || col < 0 || col > 7 || (row - dRow == originalRow && col - dCol == originalCol) || (this.boardSquares[row, col].Disc == null || this.boardSquares[row, col].Disc.Color != color))
                return false;

            return true;
        }

        public bool IsGameFinished()
        {
            if ((!ValidMoveRemaining(DiscColor.White)) && (!ValidMoveRemaining(DiscColor.Black)))
                return true;
            return false;
        }

        public override string ToString()
        {
            StringBuilder boardString = new StringBuilder();
            boardString.Append("  0 1 2 3 4 5 6 7 \n");
            for (int i = 0; i < this.boardSquares.GetLength(1); i++)
            {
                for (int j = 0; j < this.boardSquares.GetLength(0); j++)
                {
                    if (j == 0) { boardString.Append("\n" + i + " "); }
                    boardString.Append(this.boardSquares[i, j].ToString());
                }
            }
            return boardString.ToString();
        }

        public int countDiscs(DiscColor color)
        {
            int count = 0;
            foreach (Square square in this.boardSquares)
            {
                if (square.Disc.Color == color) count++;
            }
            return count;
        }
    }
}
