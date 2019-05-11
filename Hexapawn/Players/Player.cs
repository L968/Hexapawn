using Hexapawn.GameComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapawn.Players
{
    public abstract class Player
    {
        public Color Color { get; set; }

        protected Player(Color color)
        {
            Color = color;
        }
        // Class create only for inheritance
    }
}
