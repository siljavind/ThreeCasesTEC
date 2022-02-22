using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Football
    {
        private readonly int pass;
        private readonly string goal;

        public Football(int pass, string goal)
        {
            this.pass = pass; //this.x bruges i stedet for _x
            this.goal = goal.ToLower(); //ToLower så brugerinput kan være både lower- og uppercase
        }
        public string ScenarioFootball()
        {
            if (goal == "goal")
                return "\nOlé Olé Olé";

            if (pass <= 0)
                return "\nShh";

            if (pass > 0)
            {
                for (int i = 1; i <= pass; i++)
                {
                    Console.Write("Huh!");
                }
                if (pass >= 10)
                {
                    return "\nHigh Five - Jubel!!";
                }
                return "";
            }
            else
                return "";
        }
    }
}
