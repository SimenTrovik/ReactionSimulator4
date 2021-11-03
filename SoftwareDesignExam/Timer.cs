using System;
using System.Diagnostics;

namespace SoftwareDesignExam
{
    public sealed class Timer {
        private static readonly Timer instance = new();
        private Stopwatch _stopWatch = new();
        
        private Timer() {}

        public void StartTimer() {
            _stopWatch.Start();
        }

        public void StopTimer() {
            _stopWatch.Stop();
        }

        public void TimesUp() {
            _stopWatch.Reset();
        }

        public TimeSpan GetTime() {
            return _stopWatch.Elapsed;
        }

        public static Timer Instance() {
            return instance;
        }
    }

}
