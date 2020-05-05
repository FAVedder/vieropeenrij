using System;
using System.Collections.Generic;
using System.Text;
using Vieropeenrij.Model;
using Vieropeenrij.View;

namespace Vieropeenrij.Controller
{
    class NieuwSpelMenuController
    {
        private NieuwSpelMenu nieuwSpelMenu = new NieuwSpelMenu();
        private SpelController spelController = new SpelController();

        private Speler speler1;
        private Speler speler2;

        private String spelerNaam1;
        private String spelerNaam2;

        public void Start()
        {
            nieuwSpelMenu.Menu();
            spelerNaam1 = nieuwSpelMenu.spelerNaam1;
            spelerNaam2 = nieuwSpelMenu.spelerNaam2;
            MaakSpelers(spelerNaam1, spelerNaam2);
            Speelveld speelveld = new Speelveld();
            spelController.ClearSpeelVeld(speelveld);
            speelveld.beurt = Speelveld.Beurt.SPELER1;
            spelController.Start(speler1, speler2, speelveld);
        }

        //Maak twee nieuwe spelerobjecten aan met de ingevoerde namen
        public void MaakSpelers(String spelerNaam1, String spelerNaam2)
        {
            speler1 = new Speler
            {
                naam = spelerNaam1,
                score = 0,
                beurten = 0
            };

            speler2 = new Speler
            {
                naam = spelerNaam2,
                score = 0,
                beurten = 0
            };
        }
    }
}
