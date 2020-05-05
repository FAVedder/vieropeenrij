using System;
using System.Collections.Generic;
using System.Text;
using Vieropeenrij.View;
using Vieropeenrij.Util;
using Vieropeenrij.Model;

namespace Vieropeenrij.Controller
{
    class HoofdMenuController
    {
        private HoofdMenu hoofdMenu = new HoofdMenu();
        private int optie;

        public void Start()
        {
            hoofdMenu.Menu();
            optie = hoofdMenu.optie;
            switch (optie)
            {
                case 1: //Start een nieuw spel
                    NieuwSpelMenuController nieuwSpelMenuController = new NieuwSpelMenuController();
                    nieuwSpelMenuController.Start();
                    break;
                case 2: //Laad een gesaved spel
                    Game game = LoadGame.LoadAGame();
                    SpelController spelController = new SpelController();
                    spelController.Start(game.speler1, game.speler2, game.speelveld);
                    break;
                case 3: //Stop het spel
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
