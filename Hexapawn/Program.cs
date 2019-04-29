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
            try
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

                    game.Move(Player.GetMove(pawn, position));
                    DrawTable(game);
                    Console.WriteLine("O bot está pensando...");
                    Thread.Sleep(2000);
                    game.Move(Bot.GetMove(game));

                    Console.ReadKey();

                } while (game.Winner == null);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        private static void DrawTable(Game game)
        {
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
