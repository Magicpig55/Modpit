using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modpit.Node.Attributes {
    [AttributeUsage(AttributeTargets.Method)]
    public class NodeOutputAttribute : Attribute {
        public readonly NodeDataType OutputType;

        public NodeOutputAttribute(NodeDataType type) {
            OutputType = type;
        }
        public string label;
        public string Label {
            get {
                return label;
            }
            set {
                label = value;
            }
        }
    }
}
