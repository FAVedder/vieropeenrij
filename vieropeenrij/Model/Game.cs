using System;
using System.Collections.Generic;
using System.Text;

namespace Vieropeenrij.Model
{
    [Serializable]
    class Game
    {
        public Speler speler1 { get; set; }
        public Speler speler2 { get; set; }
        public Speelveld speelveld { get; set; }
                
        public Game(Speler speler1, Speler speler2, Speelveld speelveld)
        {
            this.speler1 = speler1;
            this.speler2 = speler2;
            this.speelveld = speelveld;
        }
    }
}
