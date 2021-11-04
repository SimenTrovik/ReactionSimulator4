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
            // lage delegate?
            _stopWatch.Reset();
        }

        public int GetTimeMs() {
            return _stopWatch.Elapsed.Milliseconds;
        }

        public static Timer Instance() {
            return instance;
        }
    }

}
