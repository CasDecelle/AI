using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI.Util
{
    public class Node
    {
        private LinkedList<Node> children;
        private bool isLeaf;

        public LinkedList<Node> Children
        {
            get { return this.children;  } 
        }
        public bool IsLeaf { get; set; }

        public Node()
        {
            isLeaf = true;
            children = new LinkedList<Node>();
        }

        public void AddChild(Node n)
        {
            isLeaf = false;
            children.AddFirst(n);
        }

        public Node GetChild(int i)
        {
            foreach (Node n in children)
                if (--i == 0)
                    return n;
            return null;
        }
    }
}
