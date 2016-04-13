using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

using Modpit.Util;
using Modpit.Node.Attributes;

namespace Modpit.Modifiers {
    [NodeModifier()]
    class AutoAdjust : Modifier {
        [NodeInput(NodeDataType.Image)]
        public Bitmap InputBitmap;

        [NodeOutput(NodeDataType.Image)]
        public Bitmap PerformModifier() {
            FastBitmap b = new FastBitmap(InputBitmap);
            return InputBitmap;
        }
    }
}
