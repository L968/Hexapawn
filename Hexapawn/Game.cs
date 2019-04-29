using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapawn
{
    class Game
    {
        public string[,] GamePawnsPositions { get; private set; }
        public string[] BotPaws { get; private set; }
        public string[] PlayerPawns { get; private set; }
        public string Winner { get; private set; }

        public Game()
        {
            Winner = null;
            BotPaws = new string[3] { "K1", "K2", "K3" };
            PlayerPawns = new string[3] { "P1", "P2", "P3" };

            // Fixed positions - Position being occupied
            GamePawnsPositions = new string[9, 2] {
                {"A1", BotPaws[0]},
                {"A2", "  "},
                {"A3", PlayerPawns[0]},
                {"B1", BotPaws[1]},
                {"B2", "  "},
                {"B3", PlayerPawns[1]},
                {"C1", BotPaws[2]},
                {"C2", "  "},
                {"C3", PlayerPawns[2]}
            };
        }

        public void Move(string[] move)
        {
            string selectedPawn = move[0];
            string pawnDestiny = move[1];
            int selectedPawnPosition = 0;
            int pawnDestinyPosition = 0;

            for (int i = 0; i < 9; i++)
            {
                if (selectedPawn == GamePawnsPositions[i, 1])
                {
                    selectedPawnPosition = i;
                }

                if (pawnDestiny == GamePawnsPositions[i, 0])
                {
                    pawnDestinyPosition = i;
                }
            }


            GamePawnsPositions[selectedPawnPosition, 1] = "  ";
            GamePawnsPositions[pawnDestinyPosition, 1] = selectedPawn;
        }

    }
}
