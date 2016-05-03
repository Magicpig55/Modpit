using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX.Direct2D1;
using SharpDX.Mathematics;
using _Node = Modpit.Node.Node;
using Factory2D = SharpDX.Direct2D1.Factory;

namespace Modpit {
    public static class NodeRenderer {
        public static void RenderNode(RenderTarget rt, _Node node) {
            RoundedRectangle rect = new RoundedRectangle();
            rect.Rect = SharpDX.Mathematics;
            rect.RadiusX = 5;
            rect.RadiusY = 5;
            rt.Transform = new SharpDX.Mathematics.Matrix3x2;
            rt.FillRoundedRectangle();
        }
    }
}
