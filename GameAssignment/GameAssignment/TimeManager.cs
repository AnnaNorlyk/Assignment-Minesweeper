using System;
using System.Timers;
using System.Diagnostics;

namespace GameAssignment
{
    public class TimeManager
    {
        private Stopwatch stopwatch;
        private Timer updateTimer;

        public event EventHandler<TimeSpan> TimeUpdated;

        public TimeSpan ElapsedTime => stopwatch.Elapsed; // Elapsed time as a property

        public TimeManager()
        {
            stopwatch = new Stopwatch();
            updateTimer = new Timer(1000); // Updates every second
            updateTimer.Elapsed += OnTimedEvent;
        }

        public void StartTimer()
        {
            stopwatch.Start();
            updateTimer.Start();
        }

        public void StopTimer()
        {
            stopwatch.Stop();
            updateTimer.Stop();
        }

        public void ResetTimer()
        {
            stopwatch.Reset();  
            stopwatch.Start();   
            updateTimer.Stop();  
            updateTimer.Start();
        }


        // Called every second the timer elapses
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            TimeUpdated?.Invoke(this, ElapsedTime); 
        }
    }
}
