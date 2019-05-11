﻿using Hexapawn.GameComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapawn.Players
{
    public class Human : Player
    {
        public Human(Game game, Color color) : base(game, color)
        {

        }

        public string[] GetMove(string pawn, string position)
        {
            return new string[2] { pawn, position };
        }
    }
}