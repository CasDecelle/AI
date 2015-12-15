using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Model
{
    public class Disc
    {
        private DiscColor color;

        public DiscColor Color { 
            get { return this.color; } 
            set { this.color = value;} 
        }

        public Disc(DiscColor c)
        {
            this.color = c;
        }

        public Disc(Disc d)
        {
            this.Color = d.Color == DiscColor.White ? DiscColor.White : DiscColor.Black;
        }

        public void InvertDisc() {
            switch (this.color)
            {
                case DiscColor.Black:
                    this.color = DiscColor.White;
                    break;
                case DiscColor.White:
                    this.color = DiscColor.Black;
                    break;
            }
        }

        public override string ToString()
        {
            if (this.color == DiscColor.Black)
                return "B ";
            else
                return "W ";
        }
    }
}
