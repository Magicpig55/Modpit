using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

using Modpit.Util;
using Modpit.Node.Attributes;

namespace Modpit.Modifiers {
    [NodeModifier]
    class AutoAdjust : IModifier {
        [NodeInput(NodeDataType.Image)]
        public Bitmap InputBitmap;

        [NodeOutput(NodeDataType.Image)]
        public Bitmap OutputBitmap;

        public string Name {
            get {
                return "AutoAdjust";
            }
        }

        public void Perform() {
            OutputBitmap = (Bitmap)InputBitmap.Clone();
        }
    }
}
