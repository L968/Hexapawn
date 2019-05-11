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
        public Game Game { get; private set; }
        public Color Color { get; private set; }

        protected Player(Game game, Color color)
        {
            Game = game;
            Color = color;
        }

    }
}
