using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI.Util
{
    public class Node
    {
        private Node parent;
        private LinkedList<Node> children;
        private bool isLeaf;

        public bool IsLeaf
        {
            get { return isLeaf; }
            set { isLeaf = value; }
        }

        public LinkedList<Node> Children
        {
            get { return this.children;  } 
        }
       // public bool IsLeaf { get; set; }

        public Node()
        {
            isLeaf = true;
            children = new LinkedList<Node>();
        }

        public void AddChild(Node n)
        {
            isLeaf = false;
            n.parent = this;
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
