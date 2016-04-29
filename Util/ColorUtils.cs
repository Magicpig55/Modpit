using SharpDX.Mathematics.Interop;

namespace Modpit.Util {
    public class ColorUtils {
        public static RawColor4 MakeColor(System.Drawing.Color Color) {
            return new RawColor4((float)(Color.R) / 255, (float)(Color.G) / 255, (float)(Color.B) / 255, (float)(Color.A) / 255);
        }
    }
}
