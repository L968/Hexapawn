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
        public string Name { get; private set; }
        public Game Game { get; private set; }
        public Color Color { get; private set; }

        protected Player(Game game, Color color, string name)
        {
            Game = game;
            Color = color;
            Name = name;
        }

    }
}
