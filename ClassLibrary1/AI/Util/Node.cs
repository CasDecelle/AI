using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI.Util
{
    public class Node<T>
    {
        private T data;
        private LinkedList<Node<T>> children;
        private bool isLeaf;

        public T Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        public Node(T data)
        {
            this.data = data;
            isLeaf = true;
            children = new LinkedList<Node<T>>();
        }

        public void AddChild(Node<T> n)
        {
            isLeaf = false;
            children.AddFirst(n);
        }

        public Node<T> GetChild(int i)
        {
            foreach (Node<T> t in children)
                if (--i == 0)
                    return t;
            return null;
        }
    }
}
