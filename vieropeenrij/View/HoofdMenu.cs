using System;
using System.Collections.Generic;
using System.Text;

namespace Vieropeenrij.View
{
    public class HoofdMenu
    {
        public int optie { get; set; }

        public void Menu()
        {
            System.Console.WriteLine(" _   _ _                                                  _ _ \n");
            System.Console.WriteLine("| | | (_)                                                (_|_)\n");
            System.Console.WriteLine("| | | |_  ___ _ __    ___  _ __     ___  ___ _ __    _ __ _ _ \n");
            System.Console.WriteLine("| | | | |/ _ \\ '__|  / _ \\| '_ \\   / _ \\/ _ \\ '_ \\  | '__| | |\n");
            System.Console.WriteLine("\\ \\_/ / |  __/ |    | (_) | |_) | |  __/  __/ | | | | |  | | |\n");
            System.Console.WriteLine(" \\___/|_|\\___|_|     \\___/| .__/   \\___|\\___|_| |_| |_|  |_| |\n");
            System.Console.WriteLine("                          | |                             _/ |\n");
            System.Console.WriteLine("                          |_|                            |__/\n");
            System.Console.WriteLine("1. Nieuw spel\n");
            System.Console.WriteLine("2. Spel hervatten\n");
            System.Console.WriteLine("3. Stoppen\n");
            System.Console.Write("Kies een optie: ");
            Invoer();
        }

        private void Invoer()
        {
            try
            {
                optie = Convert.ToInt32(System.Console.ReadLine());
                if (optie > 3 || optie < 1)
                {
                    System.Console.WriteLine("Kies een geldige optie.");
                    Menu();
                }
            }
            catch (ArgumentException)
            {
                System.Console.WriteLine("Voer een nummer in.");
                Menu();
            }
        }
    }
}

