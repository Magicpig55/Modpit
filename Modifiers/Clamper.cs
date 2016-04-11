using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

using Modpit.Util;

namespace Modpit.Modifiers {
    class Clamper : Generic {
        public override void PerformModifier(ref FastBitmap bitmap) {
            base.PerformModifier(ref bitmap); // Good practice to call base, it's fine if you're just overriding Generic, however.
            bitmap.Lock();
            int lr = 255, lg = 255, lb = 255;
            int ur = 0, ug = 0, ub = 0;
            for (int x = 0; x < bitmap.Width; x++) {
                for (int y = 0; y < bitmap.Height; y++) {
                    Color c = bitmap.GetPixel(x, y);
                    if (c.R < lr) lr = c.R;
                    if (c.G < lg) lg = c.G;
                    if (c.B < lb) lb = c.B;
                    if (c.R > ur) ur = c.R;
                    if (c.G > ug) ug = c.G;
                    if (c.B > ub) ub = c.B;
                }
            }
            for (int x = 0; x < bitmap.Width; x++) {
                for (int y = 0; y < bitmap.Height; y++) {
                    Color c = bitmap.GetPixel(x, y);
                    
                }
            }
        }
    }
}
