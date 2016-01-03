using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Model
{
    public class Player
    {
        private string name;
        private DiscColor color;
        private int score;

        public Player()
        {

        }

        public Player(String name, DiscColor color)
        {
            this.name = name;
            this.color = color;
        }

        public string Name { 
            get { return this.name; } 
            set { this.name = value; } 
        }

        public DiscColor Color { 
            get { return this.color; } 
            set { this.color = value; } 
        }

        public int Score { 
            get{ return this.score; }
            set { this.score = value; }
        }

        public enum Type
        {
            Human, Robot
        }

        public string ToString()
        {
            return string.Format("{0};{1}\n", name, score);
        }

    }
}
