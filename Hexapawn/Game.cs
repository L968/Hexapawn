using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapawn
{
    class Game
    {
        public string[,] BotPawnsPositions { get; private set; }
        public string[,] PlayerPawnsPositions { get; private set; }
        public string[,] GamePawnsPositions { get; private set; }

        public Game()
        {
            BotPawnsPositions = new string[3,2] {
                { "Bot1", "X" },
                { "Bot2", "X" },
                { "Bot3", "X" }
            };

            PlayerPawnsPositions = new string[3, 2] {
                { "Player1", "O"},
                { "Player2", "O"},
                { "Player3", "O"}
            };

            // Fixed positions - Position being occupied - Value to be shown in the game table
            GamePawnsPositions = new string[9, 2] {
                {"A1", BotPawnsPositions[0,1]},
                {"A2", " "},
                {"A3", PlayerPawnsPositions[0,1]},
                {"B1", BotPawnsPositions[1,1]},
                {"B2", " "},
                {"B3", PlayerPawnsPositions[1,1]},
                {"C1", BotPawnsPositions[2,1]},
                {"C2", " "},
                {"C3", PlayerPawnsPositions[2,1]}
            };
        }

        public void Move(string[] move)
        {
            
        }

    }
}
