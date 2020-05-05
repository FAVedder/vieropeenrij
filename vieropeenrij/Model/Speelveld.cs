using System;
using System.Collections.Generic;
using System.Text;

namespace Vieropeenrij.Model
{
    [Serializable]
    class Speelveld
    {
        private char[,] speelVeld = new char[6, 7];
        
        public Beurt beurt { get; set; }

        public enum Beurt
        {
            SPELER1, SPELER2
        }     

        public void SetSpeelVeld(char[,] speelVeld)
        {
            //check of het veld de juiste dimensies heeft
            if (speelVeld.GetLength(0) == 6)
            {
                if (speelVeld.GetLength(1) != 7)
                {
                    System.Console.WriteLine("Error");
                    this.speelVeld = new char[6, 7];
                }
                
                else
                {
                    this.speelVeld = speelVeld;
                }
            }
            
            else
            {
                System.Console.WriteLine("error");
                this.speelVeld = new char[6, 7];
            }
        }

        public char[,] GetSpeelveld()
        {
            return this.speelVeld;
        }
                
    }
}
