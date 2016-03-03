namespace Timer
{
    using System;
    using System.Threading.Tasks;
    using System.Timers;

    public class Timer1
    {
        public static void Main()
        {
            Task mytask = new Task(() => Console.WriteLine("hi"));
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler((object source, ElapsedEventArgs e)=> Console.WriteLine("hi"));
            // Set the Interval to 5 seconds.
            aTimer.Interval = 500;
            aTimer.Enabled = true;

            Console.WriteLine("Press \'q\' to quit the sample.");
            while (Console.Read() != 'q') ;
        }

    }
}
