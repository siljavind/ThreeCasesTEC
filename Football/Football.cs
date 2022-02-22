using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace Footy
{
    internal class Footy
    {
        static void Main(string[] args)
        {
            //Burde proppe doWhile ind, der looper TryParse på passes, indtil bruger indtaster en integer
            Console.WriteLine("How many passes?");
            string pass = Console.ReadLine();
            bool tryPass = int.TryParse(pass, out int truePass);
            if (tryPass == false)
                Console.WriteLine("Error");
            else
            {
                Console.WriteLine("\nGoal?");
                string goal = Console.ReadLine();

                Football f1 = new Football(truePass, goal);
                Console.WriteLine(f1.ScenarioFootball());
            }
            Console.ReadKey();
        }
    }
}
