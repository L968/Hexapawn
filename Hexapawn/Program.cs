using System;
using Hexapawn.Exceptions;
using Hexapawn.GameComponents;
using Hexapawn.Pieces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hexapawn.Players;

namespace Hexapawn
{
    class Program
    {
        static void Main(string[] args)
        {
            string playAgain = "N";

            // Play again loop
            do
            {
                // New game starting with default positions.
                Game game = new Game();

                do
                {
                    // Player 1 Move loop
                    while (true)
                    {
                        DrawBoard(game.Board);
                        Console.Write("Escolha a peça que será movimentada (P1, P2, P3): ");
                        string pawn = Console.ReadLine().ToUpper().Trim();
                        Console.Write("Escolha o local de destino da peça: ");
                        string position = Console.ReadLine().ToUpper().Trim();

                        try
                        {
                            game.Move(game.Player1.GetMove(pawn, position));
                        }
                        catch (MoveException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ForegroundColor = ConsoleColor.White;
                            continue;
                        }
                        break;
                    }

                    if (game.Winner != null)
                    {
                        DrawBoard(game.Board);
                        break;
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("O bot está pensando...");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1300);

                    // Player 2 Move loop
                    while (true)
                    {
                        // DrawBoard(game.Board);*
                        try
                        {
                            game.Move(game.Player2.GetMove());
                        }
                        catch (MoveException ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(ex.Message);
                            Console.ForegroundColor = ConsoleColor.White;
                            continue;
                        }
                        break;
                    }

                    if (game.Winner != null)
                    {
                        DrawBoard(game.Board);
                        break;
                    }

                } while (game.Winner == null);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"O vencedor é o {game.Winner.Name}! Deseja jogar novamente?(Digite \"S\"): ");
                Console.ForegroundColor = ConsoleColor.White;
                playAgain = Console.ReadLine().ToUpper().Trim();

            } while (playAgain == "S");
        }

        private static void DrawBoard(Board board)
        {
            Console.WriteLine("      a    b    c");
            Console.WriteLine("   +--------------+");
            Console.WriteLine($" 1 | {board.GetPieceNameOnBoard(0, 0)} | {board.GetPieceNameOnBoard(0, 1)} | {board.GetPieceNameOnBoard(0, 2)} |");
            Console.WriteLine("   +--------------+");
            Console.WriteLine($" 2 | {board.GetPieceNameOnBoard(1, 0)} | {board.GetPieceNameOnBoard(1, 1)} | {board.GetPieceNameOnBoard(1, 2)} |");
            Console.WriteLine("   +--------------+");
            Console.WriteLine($" 3 | {board.GetPieceNameOnBoard(2, 0)} | {board.GetPieceNameOnBoard(2, 1)} | {board.GetPieceNameOnBoard(2, 2)} |");
            Console.WriteLine("   +--------------+\n");
        }

    }
}
