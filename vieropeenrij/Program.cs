using System;
using Vieropeenrij.Controller;

namespace Vieropeenrij
{
    class Program
    {
        static void Main(string[] args)
        {
            HoofdMenuController hoofdMenuController = new HoofdMenuController();
            hoofdMenuController.Start();
        }
    }
}
