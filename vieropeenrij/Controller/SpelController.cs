using System;
using System.Collections.Generic;
using System.Text;
using Vieropeenrij.Model;
using Vieropeenrij.Util;
using Vieropeenrij.View;

namespace Vieropeenrij.Controller
{
    class SpelController
    {
        private EindeMenuController eindeMenuController = new EindeMenuController();

        private Speler speler1;
        private Speler speler2;
        private Speelveld speelveld;
        private SpelView spelView;

        private int laatsteIndexI; //coordinaat om vier-op-een-rij te kunnen checken
        private int laatsteIndexY; //coordinaat om vier-op-een-rij te kunnen checken
        private char spelerChar; //Speler 1 wordt met een O uitgebeeld, speler 2 met een X;

        //Een aantal variabelen die door de checkVierOpEenRij en checkWinst functies gedeeld worden
        private int aantaloprij;
        private int indexI;
        private int indexY;

        //Start een nieuw spel of opgeslagen spel met de ingevoerde spelersobjecten en speelveld
        public void Start(Speler speler1, Speler speler2, Speelveld speelveld)
        {
            this.speler1 = speler1;
            this.speler2 = speler2;
            this.speelveld = speelveld;
            spelView = new SpelView(this.speler1, this.speler2, this.speelveld);
            Spelloop();
        }

        //Maakt het speelveld 'leeg' (alle chars in de array worden '-')
        public void ClearSpeelVeld(Speelveld speelveld)
        {
            //System.Console.WriteLine("ClearSpeelVeld");

            char[,] speelVeldArray = speelveld.GetSpeelveld();
            for (int i = 0; i <= speelVeldArray.GetLength(0) - 1; i++)
            {
                for (int y = 0; y <= speelVeldArray.GetLength(1) - 1; y++)
                {

                    speelVeldArray[i, y] = '-';
                }
            }
            speelveld.SetSpeelVeld(speelVeldArray);
        }

        //Main loop van het spel, wordt uitgevoerd tot een spel over is of de speler afsluit.
        private void Spelloop()
        {
            while (true)
            {
                spelView.DrawSpeelveld();
                spelView.Invoer();
                int invoer = spelView.invoer;
                VerwerkInvoer(invoer);
                CheckVierOpEenRij();
                CheckVol();
                VeranderBeurt();
            }
        }

        //Verwerkt de invoer van de speler die aan de beurt is
        private void VerwerkInvoer(int invoer)
        {
            //System.Console.WriteLine("VerwerkInvoer");

            if (invoer == 0)
            { //Terug naar het hoofdmenu
                HoofdMenuController hoofdMenuController = new HoofdMenuController();
                hoofdMenuController.Start();
            }
            else if (invoer == 8)
            { //Spel opslaan
                Game game = new Game(speler1, speler2, speelveld);
                SaveGame.SaveAGame(game);
                System.Console.WriteLine("Spel opgeslagen.");
            }
            else
            {
                invoer--; //Haalt 1 van de invoer af, zodat de invoer overeenkomt met de index van de array

                char[,] speelveldArray = speelveld.GetSpeelveld(); //Haalt de huidige staat van het speelveld op

                if (speelveld.beurt == Speelveld.Beurt.SPELER1)
                { //Zet het juiste symbool voor de speler die aan de beurt is
                    speler1.beurten += 1;
                    spelerChar = 'O';
                }
                else
                {
                    speler2.beurten += 1; 
                    spelerChar = 'X';
                }

                //Verander het bovenste vrije vlak in de kolom met het symbool van de speler die aan de beurt is
                for (int i = 5; i >= 0; i--)
                {
                    if (speelveldArray[i, invoer] == '-')
                    {
                        speelveldArray[i, invoer] = spelerChar;
                        laatsteIndexI = i;
                        laatsteIndexY = invoer;
                        break;
                    }
                }

            }
        }

        /* Checkt of de laatste invoer van een speler voor vier op een rij zorgt. 
           Vanuit de laatste zet (coordinaat in de array) wordt zowel horizontaal,
           verticaal als diagonaal gecheckt naar vier spelersymbolen op 1 rij. 
           Omdat een spel alleen gewonnen kan worden vanuit de laatste zet, hoeft
           niet de hele array gecheckt te worden. 
           Als er een 'verkeerd' spelersymbool, leeg symbool of een out of bounds
           error verschijnt, wordt de volgende richting op gecheckt. Als er 4 of meer
           symbolen van de speler die aan de beurt is achter elkaar verschijnen eindigt
           het spel en wint de speler die aan de beurt was. 
           De loops zijn gelabeld, zodat er makkelijk vanuit inner loops gebroken kan worden
           naar een hoger niveau. 
           Ik heb wat debug prints weggecomment zodat ze makkelijk weer te activeren zijn,
           mocht er toch nog iets mis zijn. */
        private void CheckVierOpEenRij()
        {
            //System.Console.WriteLine("CheckVierOpEenRij");

            aantaloprij = 1;
            indexI = laatsteIndexI;
            indexY = laatsteIndexY;

        //System.Console.WriteLine("laatsteIndexI = " + laatsteIndexI);
        //System.Console.WriteLine("laatsteIndexY = " + laatsteIndexY);
        //check horizontaal
        
            while (indexY >= 0 && indexY <= 6)
            {

                for (int i = 0; i <= 2; i++)
                {

                    try
                    {
                        if (speelveld.GetSpeelveld()[indexI, ++indexY] == spelerChar)
                        {
                            //System.Console.WriteLine("Horizontaal + 1 eerste if");
                            aantaloprij++;
                        }
                        else
                        {

                            try
                            {
                                indexY = laatsteIndexY;
                                for (i = 0; i <= 2; i++)
                                {
                                    if (speelveld.GetSpeelveld()[indexI, --indexY] == spelerChar)
                                    {
                                        //System.Console.WriteLine("Horizontaal + 1 tweede if");
                                        aantaloprij++;
                                    }
                                    else
                                    {
                                        goto VERTICAALCHECK;
                                    }
                                }
                                goto VERTICAALCHECK;
                            }
                            catch (IndexOutOfRangeException)
                            {
                                goto VERTICAALCHECK;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {

                        indexY = laatsteIndexY;
                        for (i = 0; i <= 2; i++)
                        {
                            if (speelveld.GetSpeelveld()[indexI, --indexY] == spelerChar)
                            {
                                //System.Console.WriteLine("Horizontaal + 1 catch block");
                                aantaloprij++;
                            }
                            else
                            {
                                goto VERTICAALCHECK;
                            }
                        }
                        goto VERTICAALCHECK;
                    }
                }

            }
            

            

        //check verticaal
        VERTICAALCHECK:
            //System.Console.WriteLine("VERTICAALCHECK");
                //System.Console.WriteLine("Aantal op rij horizontaal: " + aantaloprij);
                CheckWinst();
            while (indexI >= 0 && indexI <= 5)
            {

                for (int i = 0; i <= 2; i++)
                {

                    try
                    {
                        if (speelveld.GetSpeelveld()[++indexI, indexY] == spelerChar)
                        {
                            //System.Console.WriteLine("Verticaal + 1");
                            aantaloprij++;
                        }
                        else
                        {

                            goto DIAGONAALCHECK1;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        goto DIAGONAALCHECK1;
                    }
                }

            }
            

            

        //check diagonaal1
        DIAGONAALCHECK1:
        //System.Console.WriteLine("DIAGONAALCHECK1");
            //System.Console.WriteLine("Aantal op rij verticaal: " + aantaloprij);
            CheckWinst();
            while (indexY >= 0 && indexY <= 6)
            {

                for (int i = 0; i <= 2; i++)
                {

                    try
                    {
                        if (speelveld.GetSpeelveld()[++indexI, ++indexY] == spelerChar)
                        {
                            //System.Console.WriteLine("Diagonaal1 + 1 eerste if");
                            aantaloprij++;
                        }
                        else
                        {

                            try
                            {
                                indexI = laatsteIndexI;
                                indexY = laatsteIndexY;
                                for (i = 0; i <= 2; i++)
                                {
                                    if (speelveld.GetSpeelveld()[--indexI, --indexY] == spelerChar)
                                    {
                                        //System.Console.WriteLine("Diagonaal1 + 1 tweede if");
                                        aantaloprij++;
                                    }
                                    else
                                    {
                                        goto DIAGONAALCHECK2;
                                    }
                                }
                                goto DIAGONAALCHECK2;
                            }
                            catch (IndexOutOfRangeException)
                            {
                                goto DIAGONAALCHECK2;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {

                        indexI = laatsteIndexI;
                        //System.Console.WriteLine("indexI" + indexI);
                        indexY = laatsteIndexY;
                        //System.Console.WriteLine("indexY" + indexY);
                        for (i = 0; i <= 2; i++)
                        {
                            try
                            {

                                if (speelveld.GetSpeelveld()[--indexI, --indexY] == spelerChar)
                                {
                                    //System.Console.WriteLine("Diagonaal1 + 1 catch block");
                                    aantaloprij++;
                                }
                                else
                                {
                                    goto DIAGONAALCHECK2;
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                                goto DIAGONAALCHECK2;
                            }
                        }
                        goto DIAGONAALCHECK2;
                    }
                }

            }
            

            

        //check diagonaal2
        DIAGONAALCHECK2:
        //System.Console.WriteLine("DIAGONAALCHECK2");
            //System.Console.WriteLine("Aantal op rij diagonaal1: " + aantaloprij);
            CheckWinst();
            while (indexY >= 0 && indexY <= 6)
            {

                for (int i = 0; i <= 2; i++)
                {

                    try
                    {
                        if (speelveld.GetSpeelveld()[++indexI, --indexY] == spelerChar)
                        {
                            //System.Console.WriteLine("Diagonaal2 + 1 eerste if");
                            aantaloprij++;
                        }
                        else
                        {

                            try
                            {
                                indexI = laatsteIndexI;
                                indexY = laatsteIndexY;
                                for (i = 0; i <= 2; i++)
                                {
                                    if (speelveld.GetSpeelveld()[--indexI, ++indexY] == spelerChar)
                                    {
                                        //System.Console.WriteLine("Diagonaal2 + 1 tweede if");
                                        aantaloprij++;
                                    }
                                    else
                                    {
                                        goto CheckWinst;
                                    }
                                }
                                goto CheckWinst;
                            }
                            catch (IndexOutOfRangeException)
                            {
                                goto CheckWinst;
                            }
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {

                        try
                        {
                            indexI = laatsteIndexI;
                            indexY = laatsteIndexY;

                            for (i = 0; i <= 2; i++)
                            {
                                if (speelveld.GetSpeelveld()[--indexI, ++indexY] == spelerChar)
                                {
                                    //System.Console.WriteLine("Diagonaal2 + 1 catch block");
                                    aantaloprij++;
                                }
                                else
                                {
                                    goto CheckWinst;
                                }
                            }
                            goto CheckWinst;
                        }
                        catch (IndexOutOfRangeException)
                        {
                            goto CheckWinst;
                        }
                    }
                }

            }
            //System.Console.WriteLine("Aantal op rij diagonaal2: " + aantaloprij);

            CheckWinst:
            CheckWinst();

        }

        //Een aparte functie om de winststatus te checken, voorkomt wat dubbele code in checkVierOpEenRij
        private void CheckWinst()
        {
            //System.Console.WriteLine("CheckWinst");

            if (aantaloprij >= 4)
            {
                Gewonnen();
            }
            else
            {
                aantaloprij = 1;
                indexI = laatsteIndexI;
                indexY = laatsteIndexY;
            }
        }

        //Verandert de beurt van de actieve naar de inactieve speler
        private void VeranderBeurt()
        {
            //System.Console.WriteLine("VeranderBeurt");

            if (speelveld.beurt == Speelveld.Beurt.SPELER1)
            {
                speelveld.beurt = Speelveld.Beurt.SPELER2;
            }
            else
            {
                speelveld.beurt = Speelveld.Beurt.SPELER1;
            }
            spelView.speelveld = speelveld;
        }

        //Checkt of alle vlakken in het speelveld bezet zijn en eindigt dan het spel met een gelijkspel,
        //start tenslotte het eindemenu.
        private void CheckVol()
        {
            //System.Console.WriteLine("CheckVol");

            int legevelden = 0;

            for (int i = 0; i <= speelveld.GetSpeelveld().GetLength(0) - 1; i++)
            {
                for (int y = 0; y <= speelveld.GetSpeelveld().GetLength(1) - 1; y++)
                {
                    if (speelveld.GetSpeelveld()[i, y] == '-')
                    {
                        legevelden++;
                    }
                }
            }

            if (legevelden == 0)
            {
                spelView.DrawSpeelveld();
                System.Console.WriteLine("Het speelveld is vol, gelijkspel!");
                eindeMenuController.Menu(speler1, speler2);
            }
        }

        //Feliciteer de speler die gewonnen heeft en start het eindmenu
        private void Gewonnen()
        {
            //System.Console.WriteLine("Gewonnen");

            spelView.DrawSpeelveld();
            if (speelveld.beurt == Speelveld.Beurt.SPELER1)
            {
                speler1.score++;
                System.Console.WriteLine(speler1.naam + " heeft gewonnen in " + speler1.beurten + " beurten, hoera!");
            }
            else
            {
                speler2.score++;
                System.Console.WriteLine(speler2.naam + " heeft gewonnen in " + speler2.beurten + " beurten, hoera!");
            }
            eindeMenuController.Menu(speler1, speler2);
        }
    }
}
