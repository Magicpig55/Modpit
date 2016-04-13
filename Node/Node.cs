using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Modpit.Util;
using Modpit.Modifiers;

namespace Modpit.Node {
    class Node {
        public Position Position;
        public Modifier Modifier;

        public List<NodeInput> Inputs = new List<NodeInput>();
        public List<NodeOutput> Outputs = new List<NodeOutput>();
        public List<NodeParameter> Parameters = new List<NodeParameter>();
    }
}
