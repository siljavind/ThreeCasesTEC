using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

//SE Dance.cs FØRST
namespace ClassLibrary
{
    public class DancerPoints
    {
        private readonly string name;
        private readonly int point;

        public DancerPoints(string name = null, int point = 0)
        {
            this.name = name; //this.x bruges i stedet for _x
            this.point = point;
        }
        public static void UltraConstructor()
        {
            DancerPoints dancer1 = new DancerPoints().Main();
            DancerPoints dancer2 = new DancerPoints().Main();
            Console.WriteLine("\n" + (dancer1 + dancer2).Print());
            Console.ReadKey();
        }
        private DancerPoints Main() //Metode, der efterspørger input og derefter samler dansers navn og point i et objekt
        {
            bool tryPoint = false;
            int truePoint = 0;

            TextInfo textInfo = new CultureInfo("en-UK").TextInfo; //Til brug af ToTitleCase
            Console.WriteLine("Dancer?");
            string name = textInfo.ToTitleCase(Console.ReadLine()); //Navn bliver sat til uppercase forbogstav og resten lowercase (Klassisk Navneformat)

            while (!tryPoint) //Loopes mens TryPoint ikke kan læses som integer
            {
                Console.WriteLine("Point?");
                string point = Console.ReadLine();
                tryPoint = int.TryParse(point, out truePoint);
            }

            DancerPoints dancerPoint = new DancerPoints(name, truePoint);
            return dancerPoint;
        }
        private string Print() //Metode, der udskriver de angivne variabler i en brugervenlig string 
        {
            return name + " - " + point + " point(s) in total";
        }
        public static DancerPoints operator +(DancerPoints dancer1, DancerPoints dancer2) //Bestemmer hvordan brugerdefinerede objekter inteagerer, når de møder operatoren plus   
        {
            DancerPoints combinedDancer = new DancerPoints(dancer1.name + " & " + dancer2.name, dancer1.point + dancer2.point);
            return combinedDancer;
        }

    }
}
