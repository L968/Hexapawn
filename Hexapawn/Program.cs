using System;
using System.Threading;
using Hexapawn.Exceptions;
using Hexapawn.GameComponents;
using Hexapawn.Players;

namespace Hexapawn
{
    class Program
    {

        static void Main(string[] args)
        {
            string playAgain;
            int a = 0;
            int b = 0;
            int c = 0;

            // Play again loop
            do
            {
                var game = new Game();
                playAgain = "N";
                Console.Clear();
                DrawBoard(game.Board);

                // Turn loop
                while (true)
                {
                    // Move loop
                    while (true)
                    {
                        var move = new string[2];

                        if (game.ActivePlayer is Human)
                        {
                            Console.Write("Choose a piece to be moved (P1, P2, P3): ");
                            move[0] = Console.ReadLine().ToUpper().Trim();
                            Console.Write("Choose the piece's destiny: ");
                            move[1] = Console.ReadLine().ToUpper().Trim();
                        }
                        else if (game.ActivePlayer is Bot)
                        {
                            var bot = game.ActivePlayer as Bot;
                            BotDelayMessage();
                            move = bot.GenerateMove();
                        }

                        try
                        {
                            game.Move(move);
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
                        if (game.ActivePlayer is Bot)
                        {
                            var bot = game.ActivePlayer as Bot;
                            bot.SetLastMoveActiveToZero();
                        }

                        break;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"The winner is the {game.Winner.Name}! Play one more time?(\"S\"): ");
                playAgain = Console.ReadLine().ToUpper().Trim();
                Console.ForegroundColor = ConsoleColor.White;

            } while (playAgain == "S");
        }

        private static void BotDelayMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The bot is thinking...");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(700);
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
