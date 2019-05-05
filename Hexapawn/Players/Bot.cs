using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapawn.Players
{
    public class Bot : Player
    {
        public static string[] GetMove(string[,] board)
        {
            return new string[2] {"K1", "A2"};
        }
    }
}
