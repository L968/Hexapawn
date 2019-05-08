using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapawn.Players
{
    public class Human : Player
    {
        public string[] GetMove(string pawn, string position)
        {
            return new string[2] { pawn, position };
        }
    }
}
