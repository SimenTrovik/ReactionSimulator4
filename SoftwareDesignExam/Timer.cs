using System;
using System.Diagnostics;
using System.Threading;

namespace SoftwareDesignExam
{
    public sealed class Timer {
        private static readonly Timer instance = new();
        private readonly Stopwatch _stopWatch = new();
        public int TimesUpTime => 5000;
        public int LeastRandomVal => 2000;
        public int MaxRandomVal => 5000;
        public bool FinishedTimer { get; set; }
        public int RandomTimeToStartTimer { get; set; }

        private Timer() {}

        public void StartTimer()
        {
            Thread thread1 = new Thread(StartTimerThread);
            thread1.Start();
        }

        private void StartTimerThread()
        {
            FinishedTimer = false;
            Random random = new Random();
            RandomTimeToStartTimer = random.Next(LeastRandomVal, MaxRandomVal);
            Thread.Sleep(RandomTimeToStartTimer);
            _stopWatch.Start();
            Thread thread2 = new Thread(CountDownToTimesUp);
            thread2.Start();
        }

        private void CountDownToTimesUp() {
            Thread.Sleep(TimesUpTime);
            TimesUp();
        }

        public void StopTimer() {
            _stopWatch.Stop();
        }

        public void TimesUp() {
            _stopWatch.Reset();
            FinishedTimer = true;
        }

        public int GetTimeMs() {
            return _stopWatch.Elapsed.Milliseconds + _stopWatch.Elapsed.Seconds*1000;
        }

        public static Timer Instance() {
            return instance;
        }
    }

}
