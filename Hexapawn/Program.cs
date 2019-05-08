﻿using System;
using Hexapawn.Exceptions;
using Hexapawn.GameComponents;
using Hexapawn.Pieces;
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
                DrawBoard(game.Board);
                Console.Write("Escolha a peça que será movimentada (P1, P2, P3): ");
                string pawn = Console.ReadLine().ToUpper().Trim();
                Console.Write("Escolha o local de destino da peça: ");
                string position = Console.ReadLine().ToUpper().Trim();

                try
                {
                   // game.Move(Player.GetMove(pawn, position));

                    if (game.Winner != null)
                    {
                        DrawBoard(game.Board);
                        break;
                    }
                }
                catch (MoveException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                
                DrawBoard(game.Board);
                Console.WriteLine("O bot está pensando...");
                Thread.Sleep(1500);
               // game.Move(Bot.GetMove(game.Board));

            } while (game.Winner == null);
        }

        private static void DrawBoard(Board board)
        {
            Console.WriteLine("      a    b    c");
            Console.WriteLine("   +--------------+");
            Console.WriteLine($" 1 | {board[0, 0]} | {board[0, 1]} | {board[0, 2]} |");
            Console.WriteLine("   +--------------+");
            Console.WriteLine($" 2 | {board[1, 0]} | {board[1, 1]} | {board[1, 2]} |");
            Console.WriteLine("   +--------------+");
            Console.WriteLine($" 3 | {board[2, 0]} | {board[2, 1]} | {board[2, 2]} |");
            Console.WriteLine("   +--------------+\n");
        }

    }
}
