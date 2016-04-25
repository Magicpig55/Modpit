using System.Diagnostics;

namespace Modpit {
    public class TimeHandler {
        private Stopwatch stopwatch;
        private double lastUpdate;

        public TimeHandler() {
            stopwatch = new Stopwatch();
        }
        public void Start() {
            stopwatch.Start();
            lastUpdate = 0;
        }
        public void Stop() {
            stopwatch.Stop();
        }
        public double Update() {
            double now = ElapseTime;
            double updateTime = now - lastUpdate;
            lastUpdate = now;
            return updateTime;
        }

        public double ElapseTime {
            get {
                return stopwatch.ElapsedMilliseconds * .001;
            }
        }
    }
}
