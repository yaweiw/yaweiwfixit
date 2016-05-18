namespace Timer
{
    using System;
    using System.Threading.Tasks;
    using System.Timers;

    public class Timer1
    {
        public static string GetDetails(string s)
        {
            return "s";
        }
        public static string GetDetails(char c)
        {
            return "c";
        }
        public static string GetDetails(char c, string s)
        {
            return "cs";
        }
        public static string GetDetails(string s, char c)
        {
            return "sc";
        }
        public static void Main()
        {
            string sc = GetDetails("abab", 'c');
            string cs = GetDetails('c' , "abab");
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
