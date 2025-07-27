using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Basic
{
    public class MyTimerBasic
    {
        private bool OnTick = false;
        public double TickTime = 0;
        static private Stopwatch stopwatch = new Stopwatch();
        public MyTimerBasic()
        {
            stopwatch.Start();
            this.StartTickTime(999999);
        }
        public MyTimerBasic(double TickTime)
        {
            stopwatch.Start();
            this.StartTickTime(TickTime);
        }

        private double CycleTime_start;
        public void StartTickTime(double TickTime)
        {
            this.TickTime = TickTime;
            if (!OnTick)
            {
                CycleTime_start = stopwatch.Elapsed.TotalMilliseconds;
                OnTick = true;
            }
        }

        public void StartTickTime()
        {
            if (!OnTick)
            {
                CycleTime_start = stopwatch.Elapsed.TotalMilliseconds;
                OnTick = true;
            }
        }
        public double GetTickTime()
        {
            return stopwatch.Elapsed.TotalMilliseconds - CycleTime_start;
        }
        public void TickStop()
        {
            this.OnTick = false;
        }
        public bool IsTimeOut()
        {
            //if (OnTick == false) return false;
            if ((stopwatch.Elapsed.TotalMilliseconds - CycleTime_start) >= TickTime)
            {
                OnTick = false;
                return true;
            }
            else return false;
        }



        public override string ToString()
        {
            return this.ToString(true);
        }
        public string ToString(bool retick)
        {
            string text = this.GetTickTime().ToString("0.000") + "ms";
            if (retick)
            {
                this.TickStop();
                this.StartTickTime(999999);
            }
            return text;

        }

    }
    static public class Time
    {
        static public double GetTotalMilliseconds()
        {
            return DateTime.Now.TimeOfDay.TotalMilliseconds;
        }
    }
    public class TimeLogHelper
    {
        private Stopwatch stopwatch = new Stopwatch();
        private TimeSpan lastTick;
        private List<string> segments = new List<string>();

        public TimeLogHelper()
        {
            stopwatch.Start();
            lastTick = TimeSpan.Zero;
        }

        public void Tick(string label)
        {
            var now = stopwatch.Elapsed;
            var diff = now - lastTick;
            segments.Add($"{label}:{diff.TotalMilliseconds:0.###}ms");
            lastTick = now;
        }

        public string GetResult()
        {
            return string.Join(" → ", segments);
        }

        public string Total => $"{stopwatch.Elapsed.TotalMilliseconds:0.###}ms";
    }
}
