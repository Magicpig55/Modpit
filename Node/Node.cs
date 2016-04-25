using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Modpit.Util;
using Modpit.Modifiers;

namespace Modpit.Node {
    public class Node {
        public Position Position;
        public IModifier Modifier;

        public List<NodeInput> Inputs = new List<NodeInput>();
        public List<NodeOutput> Outputs = new List<NodeOutput>();
        public List<NodeParameter> Parameters = new List<NodeParameter>();

        public void Add(NodeInput i) {
            i.Parent = this;
            Inputs.Add(i);
        }
        public void Add(NodeOutput i) {
            i.Parent = this;
            Outputs.Add(i);
        }
        public void Add(NodeParameter i) {
            i.Parent = this;
        }
    }
}
