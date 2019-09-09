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
        public Board Board { get; private set; }
        public Human Player1 { get; private set; } // Setting that player1 is a Human
        public Bot Player2 { get; private set; } // Setting that player2 is a Bot
        public Player Winner { get; private set; }
        public int Turn { get; private set; }
        private Player ActivePlayer { get; set; }
        private Player InactivePlayer { get; set; }  

        public Game()
        {
            Turn = 1;
            Player1 = new Human(this, Color.WHITE, "Human");
            Player2 = new Bot(this, Color.BLACK, "Bot");
            Board = new Board(this);
            ActivePlayer = Player1;
            InactivePlayer = Player2;
        }

        public void Move(string pieceName, string positionName)
        {
            var piece = GetPieceByName(pieceName);
            var positionIndexInBoardArray = GetPositionIndexInBoardArray(positionName);

            IsActivePlayerThePieceOwner(piece);
            piece.IsValidMove(positionIndexInBoardArray);
            MovePiece(piece, positionIndexInBoardArray);
            CheckWinner();
            SwitchActivePlayer();
            Turn++;
        }

        public void Move(string[] movement)
        {
            var piece = GetPieceByName(movement[0]);
            var positionIndexInBoardArray = GetPositionIndexInBoardArray(movement[1]);

            IsActivePlayerThePieceOwner(piece);
            piece.IsValidMove(positionIndexInBoardArray);
            MovePiece(piece, positionIndexInBoardArray);
            CheckWinner();
            SwitchActivePlayer();
            Turn++;
        }

        /// <summary>
        /// Checking if the Active Player is the Piece Owner
        /// </summary>
        /// <param name="piece">The piece to be checked</param>
        private void IsActivePlayerThePieceOwner(Piece piece)
        {
            if (piece.Owner == ActivePlayer)
            {
                return;
            }
            else
            {
                throw new MoveException("You are not the owner of this piece");
            }
        }

        /// <summary>
        /// Returns the Piece object searching for the piece's name
        /// </summary>
        /// <param name="pieceName">Piece name</param>
        /// <returns></returns>
        private Piece GetPieceByName(string pieceName)
        {
            foreach (var piece in Board.BoardArray)
            {
                if (piece == null)
                {
                    continue;
                }

                if (pieceName.Equals(piece.Name))
                {
                    return Board.BoardArray[piece.XPositionOnBoard, piece.YPositionOnBoard];
                }
            }
            throw new MoveException("Piece not found");
        }

        /// <summary>
        /// </summary>
        /// <param name="tile">Position name e.g. A1, B2</param>
        /// <returns>A int[] containing the XPositionOnBoardArray and YPositionOnBoardArray</returns>
        private int[] GetPositionIndexInBoardArray(string tile)
        {
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
                default: throw new MoveException("Board position not found");
            }
        }

        private void MovePiece(Piece piece, int[] positionIndexInBoardArray)
        {
            Board.BoardArray[piece.XPositionOnBoard, piece.YPositionOnBoard] = null;
            piece.XPositionOnBoard = positionIndexInBoardArray[0];
            piece.YPositionOnBoard = positionIndexInBoardArray[1];
            Board.BoardArray[piece.XPositionOnBoard, piece.YPositionOnBoard] = piece;
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
        }

        private void CheckWinner()
        {
            #region Win Condition 1 - Player doesn't have any pieces left
            if (!InactivePlayer.HasPieces())
            {
                Winner = ActivePlayer;
                return;
            }
            #endregion

            #region Win Condition 2 - Cross the board
            switch (ActivePlayer.Color)
            {
                case Color.WHITE:
                    foreach (var piece in Board.BoardArray)
                    {
                        if (piece == null)
                        {
                            continue;
                        }

                        if (piece.Owner.Color == Color.WHITE)
                        {
                            if (piece.XPositionOnBoard == 0)
                            {
                                Winner = ActivePlayer;
                                return;
                            }
                        }
                    }
                    break;
                case Color.BLACK:
                    foreach (var piece in Board.BoardArray)
                    {
                        if (piece == null)
                        {
                            continue;
                        }
                        
                        if (piece.Owner.Color == Color.BLACK)
                        {
                            if (piece.XPositionOnBoard == 2)
                            {
                                Winner = ActivePlayer;
                                return;
                            }
                        }
                    }
                    break;
            }
            #endregion

            #region Win Condition 3 - Next player doesn't have any moves
            foreach (var piece in Board.BoardArray)
            {
                if (piece == null)
                {
                    continue;
                }

                if (piece.Owner.Color == InactivePlayer.Color)
                {
                    if (piece.CanMove())
                    {
                        return;
                    }
                }
            }
            Winner = ActivePlayer;
            #endregion
        }

    }
}