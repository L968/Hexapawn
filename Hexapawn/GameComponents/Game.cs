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
        public Board Board { get; private set; }
        public int Turn { get; private set; }

        public Game()
        {
            Turn = 1;
            Player1 = new Human(this, Color.WHITE);
            Player2 = new Bot(this, Color.BLACK);
            Board = new Board(this);
            ActivePlayer = Player1; // First turn

        }

        public void Move(string[] move)
        {
            Piece selectedPiece = GetPieceByPositionInBoardArray(move[0]); // e.g: P1, K1
            int[] pieceDestiny = GetTilePositionInBoardArray(move[1]); // e.g: A2, B2

            IsActivePlayerThePieceOwner(selectedPiece);

            if (IsLegalMove(selectedPiece, pieceDestiny))
            {
                MovePawn(selectedPiece, pieceDestiny);
                CheckWinner();
                SwitchActivePlayer();
            }
            else
            {
                throw new MoveException("Movimento inválido.");
            }
            
        }

        private Piece GetPieceByPositionInBoardArray(string pawn)
        {
            foreach (Piece item in Board.BoardArray)
            {
                if (item == null) // Avoiding null reference exception in order to the foreach statement runs completely through the array*
                {
                    continue;
                }

                if (pawn == item.Name)
                {
                    return Board.BoardArray[item.XPositionOnBoard, item.YPositionOnBoard];
                }
            }
            throw new MoveException("Peça não encontrada");
        }

        private int[] GetTilePositionInBoardArray(string tile)
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
                default: throw new MoveException("Posição no tabuleiro não encontrada");
            }
        }

        #region Basic Validations

        private void IsActivePlayerThePieceOwner(Piece piece)
        {
            if (piece.Owner == ActivePlayer)
            {
                return;
            }
            else
            {
                throw new MoveException("Seleciona apenas suas peças");
            }
        }

        #endregion

        #region LegalMove Validations

        private bool IsLegalMove(Piece piece, int[] pieceDestiny)
        {
            


            //IsDiagonalMove(selectedPawnPositionInArray, pawnDestinyPositionInArray); // arrumar default
            //HasAPawnInDestinyPosition(selectedPawnPositionInArray);
            return true;
        }

        private bool HasAPawnInDestinyPosition(int selectedPawnPositionInArray)
        {
            //switch (selectedPawnPositionInArray)
            //{
                
            //}
            return false;
        }

        #endregion

        private void MovePawn(Piece piece, int[] pieceDestiny)
        {
            Board.BoardArray[piece.XPositionOnBoard, piece.YPositionOnBoard] = null;
            piece.XPositionOnBoard = pieceDestiny[0];
            piece.YPositionOnBoard = pieceDestiny[1];
            Board.BoardArray[piece.XPositionOnBoard, piece.YPositionOnBoard] = piece;
        }

        private void SwitchActivePlayer()
        {
            if (ActivePlayer == Player1)
            {
                ActivePlayer = Player2;
            }
            else if (ActivePlayer == Player2)
            {
                ActivePlayer = Player1;
            }
        }

        private void CheckWinner()
        {

        }
    }
}
