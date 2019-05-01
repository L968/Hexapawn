using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hexapawn
{
    class Program
    {
        static void Main(string[] args)
        {

            // New game starting with default positions.
            Game game = new Game();

            do
            {
                DrawTable(game);
                Console.Write("Escolha a peça que será movimentada (P1, P2, P3): ");
                string pawn = Console.ReadLine().ToUpper();
                Console.Write("Escolha o local de destino da peça: ");
                string position = Console.ReadLine().ToUpper();

                try
                {
                    game.Move(Player.GetMove(pawn, position));
                }
                catch (MoveException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                
                DrawTable(game);
                Console.WriteLine("O bot está pensando...");
                Thread.Sleep(1500);
                game.Move(Bot.GetMove(game));

            } while (game.Winner == null);
        }

        private static void DrawTable(Game game)
        {
            for (int i = 0; i < 9; i++)
            {
                game.GamePawnsPositions[i, 1] = game.GamePawnsPositions[i, 1] == null ? "  " : game.GamePawnsPositions[i, 1];
            }

            Console.WriteLine("      a    b    c");
            Console.WriteLine("   +--------------+");
            Console.WriteLine($" 1 | {game.GamePawnsPositions[0, 1]} | {game.GamePawnsPositions[3, 1]} | {game.GamePawnsPositions[6, 1]} |");
            Console.WriteLine("   +--------------+");
            Console.WriteLine($" 2 | {game.GamePawnsPositions[1, 1]} | {game.GamePawnsPositions[4, 1]} | {game.GamePawnsPositions[7, 1]} |");
            Console.WriteLine("   +--------------+");
            Console.WriteLine($" 3 | {game.GamePawnsPositions[2, 1]} | {game.GamePawnsPositions[5, 1]} | {game.GamePawnsPositions[8, 1]} |");
            Console.WriteLine("   +--------------+\n");
        }

    }
}
