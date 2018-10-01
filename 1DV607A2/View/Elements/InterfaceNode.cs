using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV607A2.View.Elements
{
    abstract class InterfaceNode
    {
        protected readonly InterfaceNode parent;
        protected readonly List<InterfaceNode> children;

        public void AddNode(InterfaceNode node)
        {

        }
    }
}
