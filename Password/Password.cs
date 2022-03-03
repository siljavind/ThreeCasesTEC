using ClassLibrary;
using System;
using System.IO;

namespace Password
{
    internal class Password
    {
        static void Main(string[] args)
        {
            const string path = @"database.txt"; //constant, da stien ikke skal ændres
            string username;
            string password;
            char menu;

            if (!File.Exists(path)) //Opretter ny "database", hvis filen ikke allerede findes
            {
                File.Create(path).Close();
            }

            //Burde implementere visning af krav til kodeord
            Console.WriteLine("User?");
            username = Console.ReadLine();
            Console.WriteLine("Password?");
            password = Console.ReadLine();

            ClassPassword user = new ClassPassword(path, password, username); //Opretter nyt objekt (user) af klassen ClassPassword; med parametre password & username.

            if (new FileInfo(path).Length == 0) //Hvis databasens størrelse er lig 0 bytes, skal der oprettes en ny bruger fremfor login.
            {
                if (user.PasswordCheck()) //Hvis password kan godkendes, oprettes ny bruger.
                {
                    user.NewUserToDatabase(); //Ikke helt nødvendig, men del af tidligere plan.
                    Console.WriteLine("New user added");
                }
                else
                    Console.WriteLine("Password should contain at least 1 special character, 1 number, 1 lowercase, 1 uppercase." +
                        "\nIt also cannot contain any spaces or numbers in the begging or end.");
            }
            else //Hvis databasen findes (størrelse != 0)
            {
                if (user.CorrectLogin())
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Welcome back " + username + "\n\nChange password? (Y)es (E)xit");
                        menu = Console.ReadKey().KeyChar; //KeyChar gør det muligt at bruge ReadKey, fremfor ReadLine (Måske mere forvirrende end til gavn)
                        Console.Clear();

                        switch (Char.ToUpper(menu)) //.ToUpper, da switch-case er casesensitive
                        {
                            case 'Y':
                                Console.WriteLine("New password?");
                                string changePassword = Console.ReadLine();
                                ClassPassword newPassword = new ClassPassword(path, changePassword, username);
                                if (newPassword.PasswordCheck()) //Hvis password opfylder kriterier bliver loginoplysninger i tekstfilen owerwrited
                                {
                                    using (StreamWriter line = File.CreateText(path))
                                    {
                                        line.WriteLine(username, 1, changePassword); //changePassword bruges i stedet for newPassword, da
                                                                                     //ClassPassword ikke er en string og dermed ikke kan læses af .WriteLine
                                    }
                                    Console.WriteLine("\nPassword changed");
                                    Console.ReadKey();
                                }
                                else
                                    Console.WriteLine("Password should contain at least 1 special character, 1 number, 1 lowercase, 1 uppercase." +
                                                      "\nIt also cannot contain any spaces or numbers in the begging or end.");
                                break;

                            case 'E':
                                Console.WriteLine("");
                                Environment.Exit(1);
                                break;
                        }

                    } while (!menu.Equals('E') | !menu.Equals('Y')); //Do-while, der kører indtil bruger har taget et gyldigt valg

                }
                else //Hvis login-oplysninger ikke er gyldige
                    Console.WriteLine("Username or password incorrect");
            }
            Console.ReadKey();
        }
    }
}
