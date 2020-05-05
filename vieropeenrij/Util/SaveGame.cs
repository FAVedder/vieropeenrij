using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Vieropeenrij.Model;

namespace Vieropeenrij.Util
{
    class SaveGame
    {
        private DateTime dateTime;

        String speler1Naam;
        String speler2Naam;

        public static void SaveAGame(Game game)
        {
            SaveGame saveGame = new SaveGame();
            saveGame.SaveTheGame(game);
        }

        private void SaveTheGame(Game game)
        {
            this.dateTime = DateTime.Now;
            this.speler1Naam = game.speler1.naam;
            this.speler2Naam = game.speler2.naam;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = null;

            try
            {
                stream = new FileStream(@"savegame.txt", FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, game);
            }
            catch (FileNotFoundException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            catch (IOException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                try
                {
                    if (stream != null)
                    {
                        stream.Flush();
                        stream.Close();
                    }
                }
                catch (IOException ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
