using Hexapawn.Exceptions;
using Hexapawn.Pieces;
using Hexapawn.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapawn.GameComponents
{
    public class Game
    {
        public Human Player1 { get; private set; } // Setting that player1 is a Human
        public Bot Player2 { get; private set; } // Setting that player2 is a Bot
        public Player Winner { get; private set; }
        private Player ActivePlayer { get; set; }
        private Player InactivePlayer { get; set; }
        public Board Board { get; private set; }
        public int Turn { get; private set; }

        public Game()
        {
            Turn = 1;
            Player1 = new Human(this, Color.WHITE, "Humano"); // Player 1 HAS to be WHITE, do not change it
            Player2 = new Bot(this, Color.BLACK, "Bot"); // // Player 2 HAS to be BLACK, do not change it
            Board = new Board(this);
            ActivePlayer = Player1; // First turn

        }

        public void Move(string[] move)
        {
            Piece piece = GetPieceByPositionInBoardArray(move[0]); // e.g: P1, K1
            int[] pieceDestiny = GetTilePositionInBoardArray(move[1]); // e.g: A2, B2

            IsActivePlayerThePieceOwner(piece);
            MovePiece(piece, pieceDestiny);
            CheckWinner();
            SwitchActivePlayer();
        }

        /// <summary>
        /// Checking if the Active Player is the Piece Owner
        /// </summary>
        /// <param name="piece"></param>
        private void IsActivePlayerThePieceOwner(Piece piece)
        {
            if (piece.Owner == ActivePlayer)
            {
                return;
            }
            else
            {
                throw new MoveException("Você não é o dono desta peça");
            }
        }

        /// <summary>
        /// Returns a existing Piece object on the BoardArray passing the piece's name inputed at the console as argument
        /// </summary>
        /// <param name="piece">Piece name</param>
        /// <returns></returns>
        private Piece GetPieceByPositionInBoardArray(string piece)
        {
            // Checking name for every EXISTING piece on BoardArray
            foreach (Piece item in Board.BoardArray)
            {
                // Avoiding null reference exception in order to the foreach statement runs completely through the array*
                if (item == null)
                {
                    continue;
                }

                // Check if the inputed piece name is equal to the actual item.Name
                if (piece.Equals(item.Name))
                {
                    return Board.BoardArray[item.XPositionOnBoard, item.YPositionOnBoard];
                }
            }
            throw new MoveException("Peça não encontrada");
        }

        /// <summary>
        /// Returns a int[] containing the XPositionOnBoardArray and YPositionOnBoardArray passing the position name as argument
        /// </summary>
        /// <param name="tile">Position name e.g. A1, B2</param>
        /// <returns></returns>
        private int[] GetTilePositionInBoardArray(string tile)
        {
            // Needs to be flexible enough to accept multiple board sizes :(
            switch (tile)
            {
                case "A1": return new int[2] { 0, 0 };
                case "A2": return new int[2] { 1, 0 };
                case "A3": return new int[2] { 2, 0 };
                case "B1": return new int[2] { 0, 1 };
                case "B2": return new int[2] { 1, 1 };
                case "B3": return new int[2] { 2, 1 };
                case "C1": return new int[2] { 0, 2 };
                case "C2": return new int[2] { 1, 2 };
                case "C3": return new int[2] { 2, 2 };
                default: throw new MoveException("Posição no tabuleiro não encontrada");
            }
        }

        private void MovePiece(Piece piece, int[] pieceDestiny)
        {
            if (piece.IsValidPath(pieceDestiny[0], pieceDestiny[1]))
            {
                Board.BoardArray[piece.XPositionOnBoard, piece.YPositionOnBoard] = null;
                piece.XPositionOnBoard = pieceDestiny[0];
                piece.YPositionOnBoard = pieceDestiny[1];
                Board.BoardArray[piece.XPositionOnBoard, piece.YPositionOnBoard] = piece;
            }
        }

        private void SwitchActivePlayer()
        {
            if (ActivePlayer == Player1)
            {
                ActivePlayer = Player2;
                InactivePlayer = Player1;
            }
            else if (ActivePlayer == Player2)
            {
                ActivePlayer = Player1;
                InactivePlayer = Player2;
            }
            Turn++;
        }

        private void CheckWinner()
        {
            #region Win Condition 1 - Player doesn't have any pieces left
            if (!InactivePlayer.HasPieces())
            {
                Winner = ActivePlayer;
            }
            #endregion

            #region Win Condition 2 - Cross the board
            switch (ActivePlayer.Color)
            {
                case Color.WHITE:
                    foreach (Piece item in Board.BoardArray)
                    {
                        // Avoiding null reference exception
                        if (item == null)
                        {
                            continue;
                        }

                        // Piece color has to be the same as it's owner
                        if (item.Owner.Color == Color.WHITE)
                        {
                            // Cheking when a pawn of White Team crossed to the other side of the board
                            if (item.XPositionOnBoard == 0)
                            {
                                Winner = ActivePlayer;
                            }
                        }
                    }
                    break;
                case Color.BLACK:
                    foreach (Piece item in Board.BoardArray)
                    {
                        // Avoiding null reference exceptio
                        if (item == null)
                        {
                            continue;
                        }

                        // Piece color has to be the same as it's owner
                        if (item.Owner.Color == Color.BLACK)
                        {
                            // Cheking when a pawn of Black Team crossed to the other side of the board
                            if (item.XPositionOnBoard == 2)
                            {
                                Winner = ActivePlayer;
                            }
                        }
                    }
                    break;
            }
            #endregion
        }

    }
}
