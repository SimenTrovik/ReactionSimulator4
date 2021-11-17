using System;
using System.Diagnostics;
using System.Threading;

namespace SoftwareDesignExam
{
    public sealed class Timer {
        private static readonly Timer instance = new();
        private readonly Stopwatch _stopWatch = new();
        public int TimesUpTime => GameConfig.TimesUpTime;
        public int LeastRandomVal => GameConfig.LeastRandomVal;
        public int MaxRandomVal => GameConfig.MaxRandomVal;
        public bool FinishedTimer { get; set; }
        private bool _falseStart;
        public int RandomTimeToStartTimer { get; set; }

        private Timer() {}

        public void StartTimer()
        {
            Thread thread1 = new Thread(StartTimerThread);
            thread1.Start();
        }

        private void StartTimerThread()
        {
            Random random = new Random();
            RandomTimeToStartTimer = random.Next(LeastRandomVal, MaxRandomVal);
            _falseStart = true;
            Thread.Sleep(RandomTimeToStartTimer);
            _falseStart = false;
            _stopWatch.Start();
            Thread thread2 = new Thread(CountDownToTimesUp);
            thread2.Start();
        }

        private void CountDownToTimesUp() {
            FinishedTimer = false;
            Thread.Sleep(TimesUpTime);
            TimesUp();
        }

        public int TimeLeft() {
            if (FinishedTimer || _falseStart) {
                return 0;
            } else return TimesUpTime - GetTimeMs();
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
