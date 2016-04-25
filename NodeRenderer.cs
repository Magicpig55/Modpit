using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX.Direct2D1;
using Factory2D = SharpDX.Direct2D1.Factory;

namespace Modpit {
    public class NodeRenderer : TessellationSink {
        public IDisposable Shadow { get; set; }
        PathGeometry TesselatedGeometry { get; set; }
        GeometrySink GeometrySink { get; set; }

        public NodeRenderer() {
            Factory2D factory = new Factory2D();
            RectangleGeometry geometry = new RectangleGeometry(factory, new SharpDX.Mathematics.Interop.RawRectangleF(0f, 0f, 1f, 1f));
            TesselatedGeometry = new PathGeometry(factory);
            GeometrySink = TesselatedGeometry.Open();
            GeometrySink.SetSegmentFlags(PathSegment.ForceRoundLineJoin);
            geometry.Tessellate(1, this);
        }

        public void DrawNode(RenderTarget rt, Node.Node node) {
            Brush brush = new SolidColorBrush(rt, new SharpDX.Mathematics.Interop.RawColor4(1, 1, 1, 1));
            rt.DrawGeometry(TesselatedGeometry, brush);
            brush.Dispose();
        }

        public void AddTriangles(Triangle[] triangles) {
            foreach (Triangle triangle in triangles) {
                GeometrySink.BeginFigure(triangle.Point1, FigureBegin.Filled);
                GeometrySink.AddLine(triangle.Point2);
                GeometrySink.AddLine(triangle.Point3);
                GeometrySink.EndFigure(FigureEnd.Closed);
            }
        }

        public void Close() { }

        public void Dispose() { }
    }
}
