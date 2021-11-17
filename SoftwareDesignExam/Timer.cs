using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SoftwareDesignExam
{
    public sealed class Timer {
        private static readonly Timer instance = new();
        private readonly Stopwatch _stopWatch = new();
        public bool WaitingToStart { get; set; }
        public int RandomTimeToStartTimer { get; set; }

        private Timer() {}

        public void StartTimer()
        {
            RandomTimeToStartTimer = new Random().Next(
                GameParameters.StartTimeMinimum,
                GameParameters.StartTimeMaximum
            );

            Task.Run(() =>
            {
                WaitingToStart = true;
                Thread.Sleep(RandomTimeToStartTimer);
                WaitingToStart = false;
                _stopWatch.Start();
                Task.Run(() => {
                    Thread.Sleep(GameParameters.ReactionDeadline);
                    TimesUp();
                });
            });
        }

        public int TimeLeft() {
            if (WaitingToStart)
            {
                return 0;
            } else return GameParameters.ReactionDeadline - GetTimeMs();
        }

        public void TimesUp() {
            _stopWatch.Reset();
            WaitingToStart = true;
        }

        public int GetTimeMs() {
            return _stopWatch.Elapsed.Milliseconds + _stopWatch.Elapsed.Seconds*1000;
        }

        public static Timer Instance() {
            return instance;
        }
    }
}
