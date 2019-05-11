using Hexapawn.GameComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapawn.Players
{
    public class Bot : Player
    {
        public Bot(Color color) : base(color)
        {

        }

        public string[] GetMove()
        {
            return new string[2] {"K1", "A2"};
        }
    }
}
