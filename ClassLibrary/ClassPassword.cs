using System;
using System.Linq;
using System.Text.RegularExpressions; //Regex
using System.IO; //database
using System.Globalization;
using static System.Globalization.UnicodeCategory; //For at muliggøre forkortelse af metoden Symbol()


namespace ClassLibrary
{
    public class ClassPassword
    {
        private readonly string[] database;
        private readonly string path;
        private readonly string password;
        private readonly string username;

        public ClassPassword(string path, string password, string username)
        {
            database = File.ReadAllLines(path);
            this.path = path; //this.x anvendes i stedet for _x            
            this.password = password;
            this.username = username;
        }
        public ClassPassword()
        {
        }

        public bool CorrectLogin() //Tjekker om login-oplysninger er korrekte
        {
            if ((database.First() == username) & (database.Last() == password)) //.First() & .Last() tjekker hhv. arrayets første og sidste linje          
                return true;
            else
                return false;
        }

        public void NewUserToDatabase() //Ikke nødvendig, men del af tidligere plan
        {
            using (StreamWriter file = new StreamWriter(path, true)) //true stopper overwrite af data i tekstfil(database)
            {
                file.WriteLine(username);
                file.WriteLine(password);
            }
        }
        public bool PasswordCheck() //Alle check-metoder samlet i én overordnet metode
        {
            if (Amount() & LowerUpper() & Symbol() & StartEndNumber() & NoSpaceCowboy() & Same())
                return true;
            else
                return false;
        }
        private bool Amount() //string.Length
        {
            if (password.Length >= 12)
                return true;
            else
                return false;
        }
        private bool LowerUpper() //Char.Is - foreach-loop, der kører password igennem, indtil det finder både lower- og uppercase tegn (eller løber tør for tegn)
        {
            bool result0 = false, result1 = false;

            foreach (char x in password)
            {
                if (Char.IsUpper(x))
                    result0 = true;
                if (Char.IsLower(x))
                    result1 = true;
            }
            if (result0 & result1)
                return true;
            else
                return false;
        }

        private bool Symbol() //UnicodeCategory - Hvert tegns unicode-kategori findes (en efter en) og sammenlignes med (de bestemte) UnicodeCategory.
                              //Ikke nødvendigvis den mest passende eller produktive måde at løse problemet på, men lærerigt.
        {
            foreach (char x in password)
            {
                UnicodeCategory[] category = { CurrencySymbol, MathSymbol, OtherPunctuation, ModifierSymbol, OtherSymbol, OpenPunctuation, ClosePunctuation, DashPunctuation, ConnectorPunctuation };
                if (category.Contains(CharUnicodeInfo.GetUnicodeCategory(x)))
                    return true;
            }
            return false;
        }
        private bool StartEndNumber() //Regex - Regex101.com
        {
            Regex regex = new Regex(@"^[^0-9].*[0-9]+.*[^0-9]$"); //Kontrollerer om der er tal i starten, midten og slutningen. Tillader dem kun i midten.
            Match match = regex.Match(password); //Password matches op mod de definerede Regex-"regler"

            if (match.Success)
                return true;
            else
                return false;
        }
        private bool NoSpaceCowboy() //Regex (igen) - Kunne tilføjes til StartEndNumber(). Opgavebeskrivelse kaldte efter en metode for sig selv.
        {
            Regex regex = new Regex(@"\s");
            Match match = regex.Match(password);

            if (!match.Success) //Omvendt af tidligere Regex (!)
                return true;
            else
                return false;
        }
        private bool Same() //Easy peasy. Sammenligner password & username.
        {
            if (password.ToLower() != username.ToLower())
                return true;
            else
                return false;
        }
    }
}