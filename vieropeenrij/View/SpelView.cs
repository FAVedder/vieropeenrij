using System;
using System.Collections.Generic;
using System.Text;
using Vieropeenrij.Model;

namespace Vieropeenrij.View
{
    class SpelView
    {
        public Speler speler1 { get; set; }
        public Speler speler2 { get; set; }
        public Speelveld speelveld { get; set; }

        public int invoer { get; set; }

        public SpelView(Speler speler1, Speler speler2, Speelveld speelveld)
        {
            this.speler1 = speler1;
            this.speler2 = speler2;
            this.speelveld = speelveld;
        }

        public void DrawSpeelveld()
        {
            string naamHuidigeSpeler;

            if (speelveld.beurt == Speelveld.Beurt.SPELER1)
            {
                naamHuidigeSpeler = speler1.naam;
            }
            else
            {
                naamHuidigeSpeler = speler2.naam;
            }

            System.Console.WriteLine("Speler 1: " + speler1.naam + " O (score {0})\tSpeler 2: " + speler2.naam + " X (score {1})\n", speler1.score, speler2.score);
            System.Console.WriteLine("Aan de beurt: " + naamHuidigeSpeler);
            System.Console.WriteLine("---------------");
            System.Console.WriteLine(" 1 2 3 4 5 6 7");
            for (int i = 0; i <= speelveld.GetSpeelveld().GetLength(0) - 1; i++)
            {
                for (int y = 0; y <= speelveld.GetSpeelveld().GetLength(1) - 1; y++)
                {
                    System.Console.Write(" " + speelveld.GetSpeelveld()[i, y]);
                }
                System.Console.WriteLine("\n");
            }
            System.Console.WriteLine("---------------");

        }

        public void Invoer()
        {
            try
            {
                System.Console.WriteLine("Kies een kolom, 8 om op te slaan, of 0 om af te sluiten");
                invoer = Convert.ToInt32(System.Console.ReadLine());
                if (invoer > 8 || invoer < 0)
                {
                    System.Console.WriteLine("Kies een geldige optie.");
                    Invoer();
                }
                else if (invoer == 0 || invoer == 8)
                {
                    //doe niets
                }
                else if (speelveld.GetSpeelveld()[0, invoer - 1] == 'O' || speelveld.GetSpeelveld()[0, invoer - 1] == 'X')
                {
                    System.Console.WriteLine("Kolom is vol, kies een andere.");
                    DrawSpeelveld();
                    Invoer();
                }
            }
            catch (ArgumentException)
            {
                System.Console.WriteLine("Voer een nummer in.");
                Invoer();
            }

        }
    }
}
