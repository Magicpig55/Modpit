using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modpit.Node.Attributes {
    [AttributeUsage(AttributeTargets.Field)]
    class NodeInputAttribute : Attribute {
        public readonly NodeDataType InputType;
        public NodeInputAttribute(NodeDataType type) {
            InputType = type;
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
