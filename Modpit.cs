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
using Modpit.Util;
using System.Drawing;

//using SharpDX.Direct2D1;

namespace Modpit {
    // Modpit contains all necessary data for the workflow, as well as
    // form controls.
    class Modpit : SimpleWindow {

        private static List<Type> modifiers = new List<Type>();

        public static Modpit Instance;

        [STAThread]
        public static void Main(string[] args) {
            Instance = new Modpit();
            Instance.Run();
        }

        private NodeRenderer nr = new NodeRenderer();
        private Node.Node testNode = CreateNode(typeof(AutoAdjust));

        private float MX = 0f;
        private float MY = 0f;

        public Modpit() {
            // Scan for and add all Modifiers to a list
            //foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies()) {
            //    foreach (Type type in asm.GetTypes()) {
            //        if (type.GetCustomAttributes(typeof(NodeModifierAttribute), true).Length > 0 && type.IsSubclassOf(typeof(IModifier))) {
            //            modifiers.Add(type);
            //        }
            //    }
            //}
        }
        public static Node.Node CreateNode(Type ModType, Util.Position Pos) {
            //if (!ModType.IsSubclassOf(typeof(IModifier))) throw new Exception();
            Node.Node n = new Node.Node();
            n.Modifier = (IModifier)Activator.CreateInstance(ModType);
            n.Position = Pos;
            return n;
        }
        public static Node.Node CreateNode(Type ModType) {
            return CreateNode(ModType, new Util.Position(0, 0));
        }
        protected override void Draw(TimeHandler time) {
            base.Draw(time);
            RenderTarget2D.Clear(ColorUtils.MakeColor(Color.DarkGray));
            RenderTarget2D.FillRectangle(new SharpDX.Mathematics.Interop.RawRectangleF(MX - 5f, MY - 5f, MX + 5f, MY + 5f), SceneColorBrush);
        }

        protected override void MouseMove(MouseEventArgs e) {
            base.MouseMove(e);
            MX = e.X;
            MY = e.Y;
        }
    }
}
