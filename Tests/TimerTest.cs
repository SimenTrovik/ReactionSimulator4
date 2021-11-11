using NUnit.Framework;
using System;
using System.Threading;
using Timer = SoftwareDesignExam.Timer;

namespace Tests
{
    public class TimerTest
    {
        private readonly int _waitTime = 10;
        private readonly Timer _timer = Timer.Instance();
        
        [TearDown]
        public void Cleanup() {
            _timer.TimesUp();
        }
        
        [Test]
        public void ShouldBehaveAsSingleton() {
            Timer timer1 = Timer.Instance();
            Timer timer2 = Timer.Instance();
            Assert.That(timer1 == timer2);
        }

        [Test]
        public void ShouldStart() {
            _timer.StartTimer();
            Thread.Sleep(_waitTime);
            Thread.Sleep(_timer.RandomTimeToStartTimer+100);
            double x = _timer.GetTimeMs();
            Assert.That(x > 0);
        }

        [Test]
        public void ShouldStop() {
            _timer.StartTimer();
            Thread.Sleep(_waitTime);
            _timer.StopTimer();
            double x = _timer.GetTimeMs();
            Thread.Sleep(_waitTime);
            double y = _timer.GetTimeMs();
            Assert.AreEqual(x, y);
        }

        [Test]
        public void ShouldReset() {
            _timer.StartTimer();
            Thread.Sleep(_waitTime);
            _timer.TimesUp();
            int x = _timer.GetTimeMs();
            Assert.That(x == 0);
        }

        [Test]
        public void ShouldGetTimeMs()
        {
            _timer.StartTimer();
            Thread.Sleep(_waitTime);
            Assert.That(_timer.GetTimeMs() == 0);
            Thread.Sleep(_timer.RandomTimeToStartTimer);
            Assert.That(_timer.GetTimeMs() > 0 && _timer.GetTimeMs() < 100);
        }

        [Test]
        public void ShouldResetAfterCountDownToTimesUp()
        {
            _timer.StartTimer();
            Thread.Sleep(_waitTime);
            Thread.Sleep(_timer.RandomTimeToStartTimer+_timer.TimesUpTime);
            Assert.That(_timer.GetTimeMs() == 0);
        }

        //[Test]
        public void ShouldUpdateFinishedTimer()
        {
            _timer.StartTimer();
            Thread.Sleep(_waitTime);
            Assert.That(_timer.FinishedTimer == false);
            _timer.TimesUp();
            Thread.Sleep(_waitTime);
            Assert.That(_timer.FinishedTimer);
            _timer.StartTimer();
            Thread.Sleep(_waitTime);
            Assert.That(_timer.FinishedTimer == false);
        }
    }
}
