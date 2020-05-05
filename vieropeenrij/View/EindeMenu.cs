using System;
using System.Collections.Generic;
using System.Text;

namespace Vieropeenrij.View
{
    class EindeMenu
    {
        public int optie { get; set; }

        public void Menu()
        {
            System.Console.WriteLine("Het spel is afgelopen. Nog een keer spelen?\n");
            System.Console.WriteLine("1. Nog een spel met dezelfde spelers\n");
            System.Console.WriteLine("2. Nog een spel met andere spelers\n");
            System.Console.WriteLine("3. Terug naar het hoofdmenu\n");
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
