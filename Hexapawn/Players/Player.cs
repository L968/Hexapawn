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
        public string Name { get; private set; } // Player nickname to be shown
        public Game Game { get; private set; } // Game in which this player is playing
        public Color Color { get; private set; } // Player team

        protected Player(Game game, Color color, string name)
        {
            Game = game;
            Color = color;
            Name = name;
        }

    }
}
