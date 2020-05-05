using System;
using System.Collections.Generic;
using System.Text;

namespace Vieropeenrij.View
{
    class NieuwSpelMenu
    {
        public string spelerNaam1 { get; set; }
        public string spelerNaam2 { get; set; }

        public void Menu()
        {
            try
            {
                System.Console.Write("Voer naam speler 1 in: ");
                spelerNaam1 = System.Console.ReadLine();

                System.Console.Write("Voer naam speler 2 in: ");
                spelerNaam2 = System.Console.ReadLine();
            }
            catch (ArgumentException)
            {
                System.Console.WriteLine("Voer een geldige naam in.");
                Menu();
            }
        }
    }
}
