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
            string playAgain;

            // Play again loop
            do
            {
                // New game starting with default positions.
                Game game = new Game();
                playAgain = "N";
                Console.Clear();
                DrawBoard(game.Board);

                // Game loop
                while (true)
                {
                    // Player 1 Move loop
                    while (true)
                    {
                        Console.Write("Choose a piece to be moved (P1, P2, P3): ");
                        string pawn = Console.ReadLine().ToUpper().Trim();
                        Console.Write("Choose the piece destiny: ");
                        string position = Console.ReadLine().ToUpper().Trim();

                        try
                        {
                            game.Move(game.Player1.GetMove(pawn, position));
                            DrawBoard(game.Board);
                            break;
                        }
                        catch (MoveException ex)
                        {
                            ShowMoveExpectionMessage(ex.Message);
                            continue;
                        }
                        catch (Exception ex)
                        {
                            ShowExpectionMessage(ex.Message);
                            continue;
                        }
                    }

                    if (game.Winner != null)
                    {
                        break;
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("O bot está pensando...");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);

                    // Player 2 Move loop
                    while (true)
                    {
                        try
                        {
                            game.Move(game.Player2.GetMove());
                            DrawBoard(game.Board);
                            break;
                        }
                        catch (MoveException ex)
                        {
                            ShowMoveExpectionMessage(ex.Message);
                            continue;
                        }
                        catch (Exception ex)
                        {
                            ShowExpectionMessage(ex.Message);
                            continue;
                        }
                    }

                    if (game.Winner != null)
                    {
                        break;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"O vencedor é o {game.Winner.Name}! Deseja jogar novamente?(Digite \"S\"): ");
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

        private static void ShowMoveExpectionMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void ShowExpectionMessage(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(message);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
