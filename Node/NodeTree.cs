using Modpit.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modpit.Node {
    public class NodeTree {
        public List<Node> NodeList = new List<Node>();

        public Node CreateNode(Type ModType) {
            return CreateNode(ModType, new Util.Position(0, 0));
        }
        public static Node CreateNode(Type ModType, Util.Position Pos) {
            //if (!ModType.IsSubclassOf(typeof(IModifier))) throw new Exception();
            Node n = new Node();
            n.Modifier = (IModifier)Activator.CreateInstance(ModType);
            n.Position = Pos;
            return n;
        }
    }
}
