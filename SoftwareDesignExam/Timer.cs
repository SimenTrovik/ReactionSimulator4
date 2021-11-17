using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SoftwareDesignExam
{
    public sealed class Timer {
        private static readonly Timer Instance = new();
        private readonly Stopwatch _stopWatch = new();
        public bool WaitingToStart { get; set; }
        public int RandomTimeToStartTimer { get; set; }

        private Timer() {}

        public void StartTimer()
        {
            RandomTimeToStartTimer = new Random().Next(
                GameConfig.StartTimeMinimum,
                GameConfig.StartTimeMaximum
            );

            Task.Run(() =>
            {
                WaitingToStart = true;
                Thread.Sleep(RandomTimeToStartTimer);
                WaitingToStart = false;
                _stopWatch.Start();
                Task.Run(() => {
                    Thread.Sleep(GameConfig.ReactionDeadline);
                    TimesUp();
                });
            });
        }

        public int TimeLeft()
        {
            return WaitingToStart ? 0 : GameConfig.ReactionDeadline - GetTimeMs();
        }

        public void TimesUp() {
            _stopWatch.Reset();
            WaitingToStart = true;
        }

        public int GetTimeMs() {
            return _stopWatch.Elapsed.Milliseconds + _stopWatch.Elapsed.Seconds*1000;
        }

        public static Timer GetInstance() {
            return Instance;
        }
    }
}
