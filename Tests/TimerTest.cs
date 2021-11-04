using NUnit.Framework;
using SoftwareDesignExam;
using System;

namespace Tests
{
    public class Tests
    {
        int waitTime = 10;
        Timer timer = Timer.Instance();
        
        [TearDown]
        public void Cleanup() {
            timer.TimesUp();
        }
        
        [Test]
        public void ShouldBehaveAsSingleton() {
            Timer timer1 = Timer.Instance();
            Timer timer2 = Timer.Instance();
            Assert.That(timer1 == timer2);
        }

        [Test]
        public void ShouldStart() {
            timer.StartTimer();
            System.Threading.Thread.Sleep(waitTime);
            double x = timer.GetTimeMs();
            Assert.That(x > 0);
        }

        [Test]
        public void shouldStop() {
            timer.StartTimer();
            System.Threading.Thread.Sleep(waitTime);
            timer.StopTimer();
            double x = timer.GetTimeMs();
            System.Threading.Thread.Sleep(waitTime);
            double y = timer.GetTimeMs();
            Assert.AreEqual(x, y);
        }

        [Test]
        public void ShouldReset() {
            timer.StartTimer();
            System.Threading.Thread.Sleep(waitTime);
            timer.TimesUp();
            int x = timer.GetTimeMs();
            Assert.That(x == 0);
        }

    }
}
