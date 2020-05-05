using System;
using System.Collections.Generic;
using System.Text;
using Vieropeenrij.Model;
using Vieropeenrij.View;

namespace Vieropeenrij.Controller
{
    class EindeMenuController
    {
        private EindeMenu eindeMenu = new EindeMenu();
        private int optie;

        public void Menu(Speler speler1, Speler speler2)
        {
            eindeMenu.Menu();
            optie = eindeMenu.optie;
            switch (optie)
            {
                case 1: //Start nog een spel met dezelfde spelers
                    speler1.beurten = 0;
                    speler2.beurten = 0;
                    SpelController spelController = new SpelController();
                    Speelveld speelveld = new Speelveld();
                    spelController.ClearSpeelVeld(speelveld);
                    spelController.Start(speler1, speler2, speelveld);
                    break;
                case 2: //Start nog een spel met nieuwe spelers
                    NieuwSpelMenuController nieuwSpelMenuController = new NieuwSpelMenuController();
                    nieuwSpelMenuController.Start();
                    break;
                case 3: //Ga terug naar het hoofdmenu
                    HoofdMenuController hoofdMenuController = new HoofdMenuController();
                    hoofdMenuController.Start();
                    break;
            }
        }
    }
}
