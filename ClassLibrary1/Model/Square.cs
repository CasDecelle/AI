using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Model
{
    public class Square
    {
        private int col;
        private int row;
        private Disc disc;

        public int Col {
            get { return this.col; }
            set { this.col = value; }
        }
        public int Row
        {
            get { return this.row; }
            set { this.row = value; }
        }
        public Disc Disc
        {
            get { return this.disc; }
            set { this.disc = value; }
        }

        public Square() { }

        public Square(int col, int row)
        {
            this.col = col;
            this.row = row;
        }
        
        /*
         * Copy constructor 
        */
        public Square(Square sq) { this.col = sq.col; this.row = sq.row; this.disc = new Disc(sq.Disc); }

        public bool isOccupied(Square sq) { return this.disc != null; }

        public override string ToString()
        {
            if (disc == null)
            {
                return "_ ";
            }
            else
            {
                return disc.ToString();
            }
        }
    }
}
