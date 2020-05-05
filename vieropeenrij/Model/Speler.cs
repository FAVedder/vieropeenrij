using System;
using System.Collections.Generic;
using System.Text;

namespace Vieropeenrij.Model
{
    [Serializable]
    class Speler
    {
        public string naam { get; set; }
        public int score { get; set; }
        public int beurten { get; set; }
    }
}
