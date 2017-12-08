using System;
using System.Configuration;

namespace DummyForTC
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /*    No Comment :) (╯°□°）╯︵ ┻━┻) --------- :D :D */
                Console.WriteLine("Build me TeamCity!\n\nThis is an awesome test branch, the best I've seen");
                Console.WriteLine("Go on, flip that table! (╯°□°）╯︵ ┻━┻) - rahhhhhhhhhhhhhhhhhhhh");

                var url = ConfigurationManager.AppSettings["BaseAdslotUrl"];
                Console.WriteLine($"BaseAdslotUrl = {url}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }
    }
}
