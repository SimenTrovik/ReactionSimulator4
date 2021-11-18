using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SoftwareDesignExam
{
    public sealed class Timer
    {
        #region Fields
        private static readonly Timer Instance = new();
        private readonly Stopwatch _stopWatch = new();
        public bool WaitingToStart { get; set; }
        public int PreTimer { get; set; }
        #endregion

        #region Constructor
        private Timer() { }
        #endregion

        #region Methods
        // Used for getting the instance of this Singleton
        public static Timer GetInstance()
        {
            return Instance;
        }
        public void StartTimer()
        {
            // Creates a random interval of time, to make prediction more difficult
            PreTimer = new Random().Next(
                GameConfig.StartTimeMinimum,
                GameConfig.StartTimeMaximum
            );

            // Starts the timer on a thread
            Task.Run(() =>
            {
                WaitingToStart = true;
                Thread.Sleep(PreTimer);
                WaitingToStart = false;
                _stopWatch.Start();
                // Starts another thread with the countdown timer
                Task.Run(() =>
                {
                    Thread.Sleep(GameConfig.ReactionDeadline);
                    TimesUp();
                });
            });
        }

        // Returns the time since the countdown started
        public int TimeLeft()
        {
            return WaitingToStart ? 0 : GameConfig.ReactionDeadline - GetTimeMs();
        }

        // Resets the values of the timer
        public void TimesUp()
        {
            _stopWatch.Reset();
            WaitingToStart = true;
        }
        public int GetTimeMs()
        {
            return _stopWatch.Elapsed.Milliseconds + _stopWatch.Elapsed.Seconds * 1000;
        }
        #endregion
    }
}