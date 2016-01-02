using System;
using System.Collections.Generic;
using System.IO;
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

        public static void Print(Node node, int depth)
        {
            string tabs = "";
            for (int i = 0; i < depth; i++)
            {
                tabs += "\t";
            }

            File.AppendAllText("Tree.txt", tabs + node.ToString());

            if (node.Children == null || node.Children.Count == 0) return;
            foreach (var child in node.Children)
            {
                Print(child, depth + 1);
            }
        }
    }
}
