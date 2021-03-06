﻿using System;
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

        public static string Print(Node node, string result, int depth)
        {
            string tabs = "";
            for (int i = 0; i < depth; i++)
            {
                tabs += "\t";
            }

            result += tabs + node.ToString();

            if (node.Children == null || node.Children.Count == 0) return result;
            foreach (var child in node.Children)
            {
                 result = Print(child, result, depth + 1);
            }
            return result;
        }
    }
}
