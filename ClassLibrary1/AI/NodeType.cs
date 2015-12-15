using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.AI
{
    public enum NodeType 
    { 
        MAX_NODE, MIN_NODE
    }

    public static class NodeTypeExtensions
    {
        public static NodeType GetOppositeType(NodeType type)
        {
            if (type == NodeType.MAX_NODE) return NodeType.MIN_NODE;
            return NodeType.MAX_NODE;
        }
    }
}
