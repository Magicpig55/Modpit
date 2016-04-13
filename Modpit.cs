using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

using Modpit.Modifiers;
using Modpit.Node.Attributes;
using Modpit.Node;

namespace Modpit {
    // Modpit contains all necessary data for the workflow, as well as
    // form controls.
    class Modpit {

        private static List<Type> modifiers = new List<Type>();

        public static Modpit Instance;

        public static void Main(string[] args) {
            Instance = new Modpit();
        }

        public Modpit() {
            // Scan for and add all Modifiers to a list
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies()) {
                foreach (Type type in asm.GetTypes()) {
                    if (type.GetCustomAttributes(typeof(NodeModifierAttribute), true).Length > 0 && type.IsSubclassOf(typeof(Modifier))) {
                        modifiers.Add(type);
                    }
                }
            }
        }
        public static Node.Node CreateNode(Type ModType) {
            Node.Node n = new Node.Node();
            n.Modifier = (Modifier)Activator.CreateInstance(ModType);
            n.Position = new Util.Position(0, 0);
            return n;
        }
    }
}
