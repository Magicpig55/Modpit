using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modpit.Node {
    class NodeInput {
        public NodeOutput Output = null;
        public Node Parent = null;
        public bool Connected = false;
    }
}
