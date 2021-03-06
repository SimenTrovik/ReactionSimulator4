using NUnit.Framework;
using System;
using System.Threading;
using Timer = SoftwareDesignExam.Timer;
using SoftwareDesignExam;

namespace Tests
{
    public class TimerTest
    {
        private readonly int _waitTime = 3; // For threads to catch up
        private readonly Timer _timer = Timer.GetInstance();

        [TearDown]
        public void Cleanup()
        {
            _timer.TimesUp();
        }

        [Test]
        public void ShouldBehaveAsSingleton()
        {
            Timer timer1 = Timer.GetInstance();
            Timer timer2 = Timer.GetInstance();
            Assert.That(timer1 == timer2);
        }

        [Test]
        public void ShouldStart()
        {
            _timer.StartTimer();
            Thread.Sleep(_waitTime);
            Thread.Sleep(_timer.PreTimer + 100);
            double x = _timer.GetTimeMs();
            Assert.That(x > 0);
        }

        [Test]
        public void ShouldReset()
        {
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
            Thread.Sleep(_timer.PreTimer);
            Console.WriteLine("Time: "+_timer.GetTimeMs());
            Thread.Sleep(1);
            Assert.That(_timer.GetTimeMs() > 0 && _timer.GetTimeMs() < 100);
        }

        [Test]
        public void ShouldResetAfterCountDownToTimesUp()
        {
            _timer.StartTimer();
            Thread.Sleep(_waitTime);
            Thread.Sleep(_timer.PreTimer + GameConfig.ReactionDeadline + 100);
            Assert.That(_timer.GetTimeMs() == 0);
        }
    }
}
