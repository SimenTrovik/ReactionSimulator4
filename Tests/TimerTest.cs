using NUnit.Framework;
using SoftwareDesignExam;
using System;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void ShouldBehaveAsSingleton() {
            Timer timer1 = Timer.Instance();
            Timer timer2 = Timer.Instance();
            Assert.That(timer1 == timer2);
        }

        [Test]
        public void ShouldStartAndPause() {
            int waitTime = 10;
            Timer timer = Timer.Instance();
            int x = (int)timer.GetTime().TotalMilliseconds;
            Assert.That(x == 0); // Stopwatch has not yet started

            // Let stopwatch run for some time
            timer.StartTimer();
            System.Threading.Thread.Sleep(waitTime);
            timer.StopTimer();

            // Collect time for x, wait a bit and collect timespan for y
            x = (int)timer.GetTime().TotalMilliseconds;
            System.Threading.Thread.Sleep(waitTime);
            int y = (int)timer.GetTime().TotalMilliseconds;

            Assert.That(x > waitTime);  // Stopwatch has started
            Assert.AreEqual(x, y);      // Stopwatch has stopped
        }

        [Test]
        public void ShouldReset() {
            int waitTime = 10;
            Timer timer = Timer.Instance();

            // Let stopwatch run for some time, then reset
            timer.StartTimer();
            System.Threading.Thread.Sleep(waitTime);
            timer.TimesUp();

            int x = (int)timer.GetTime().TotalMilliseconds;

            Assert.That(x == 0);
        }

    }
}
