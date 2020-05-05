using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Vieropeenrij.Controller;
using Vieropeenrij.Model;

namespace Vieropeenrij.Util
{
    class LoadGame
    {
        public static Game LoadAGame()
        {
            LoadGame loadGame = new LoadGame();
            Game game = loadGame.LoadTheGame();
            return game;
        }

        private Game LoadTheGame()
        {
            Game game = null;

            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(@"savegame.txt", FileMode.Open, FileAccess.Read);
                game = (Game)formatter.Deserialize(stream);
            } 
            catch (FileNotFoundException) //Als er nog geen save bestand is weer terug naar het hoofdmenu
            { 
                System.Console.WriteLine("Geen savegame gevonden.");
                HoofdMenuController hoofdMenuController = new HoofdMenuController();
                hoofdMenuController.Start();
            }
            catch (IOException ex)
            {
                System.Console.WriteLine(ex);
            }

            return game;
            
        }
            
    }
}
