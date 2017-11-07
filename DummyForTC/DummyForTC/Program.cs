using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyForTC
{
    class Program
    {
        static void Main(string[] args)
        {
            /*    No Comment :) (╯°□°）╯︵ ┻━┻) --------- :D*/
            Console.WriteLine("Build me TeamCity!\n\nThis is an awesome test branch, the best I've seen");
            Console.WriteLine("Go on, flip that table! (╯°□°）╯︵ ┻━┻) - rahhhhhhhhhhhhhhhhhhhh");

            var url = ConfigurationManager.AppSettings["BaseAdslotUrl"];
            Console.WriteLine($"BaseAdslotUrl = {url}");
            Console.ReadLine();
        }
    }
}
