using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapawn
{
    class Program
    {
        static void Main(string[] args)
        {
            // New game starting with default positions.
            Game game = new Game();
            DrawTable(game);

            string pawn;
            string position;

            Console.WriteLine("Escolha o local da peça que será movimentada:\n");
            pawn = Console.ReadLine();
            Console.WriteLine("Escolha o local de destino da peça:\n");
            position = Console.ReadLine();

            game.Move(Player.GetMove(pawn,position));
            game.Move(Bot.GetMove(game));

            Console.ReadKey();
        }

        private static void DrawTable(Game game)
        {
            Console.WriteLine("     a   b   c");
            Console.WriteLine("   +-----------+");
            Console.WriteLine($"1  | {game.GamePawnsPositions[0, 1]} | {game.GamePawnsPositions[3, 1]} | {game.GamePawnsPositions[6, 1]} |");
            Console.WriteLine($"2  | {game.GamePawnsPositions[1, 1]} | {game.GamePawnsPositions[4, 1]} | {game.GamePawnsPositions[7, 1]} |");
            Console.WriteLine($"3  | {game.GamePawnsPositions[2, 1]} | {game.GamePawnsPositions[5, 1]} | {game.GamePawnsPositions[8, 1]} |");
            Console.WriteLine("   +-----------+");
        }

    }
}
