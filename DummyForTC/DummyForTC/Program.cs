using System.Text.RegularExpressions;
using System;
using System.Configuration;

namespace DummyForTC
{
    class Program
    {
        static void Main(string[] args)
        {
#warning, this program is going to do stuff!
            try
            {
                /*    No Comment :) (╯°□°）╯︵ ┻━┻) --------- :D */
                Console.WriteLine("Build me TeamCity!\n\nThis is an awesome test branch, the best I've seen");
                Console.WriteLine("Go on, flip that table! (╯°□°）╯︵ ┻━┻) - rahhhhhhhhhhhhhhhhhhhh");

                var url = ConfigurationManager.AppSettings["BaseAdslotUrl"];
                Console.WriteLine($"BaseAdslotUrl = {url}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
#warning warning warning, rawhide!

            // Gimme that warnings report! :)
#warning This is a deliberate warning to test teamcity
            Console.ReadLine(); ; ; ; ;
        }
    }
}
