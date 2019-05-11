using System;
using Hexapawn.Pieces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapawn.GameComponents
{
    public class Board
    {
        // A array of pieces representing the board
        public Piece[,] BoardArray { get; set; }
        public Game Game { get; set; } // Receives the game object to access the Player object and give them their pieces

        public Board(Game game)
        {
            BoardArray = new Piece[3, 3];
            Game = game;
            SetPieces();
        }

        private void SetPieces()
        {
            /*
                 a    b    c 
               +--------------+
             1 | K1 | K2 | K3 |
               +--------------+
             2 |    |    |    |
               +--------------+
             3 | P1 | P2 | P3 |
               +--------------+
             */

            Piece pawn1 = new Pawn(2, 0, Game.Player1, this, "P1"); // A3
            Piece pawn2 = new Pawn(2, 1, Game.Player1, this, "P2"); // B3
            Piece pawn3 = new Pawn(2, 2, Game.Player1, this, "P3"); // C3

            Piece pawn4 = new Pawn(0, 0, Game.Player2, this, "K1"); // A1
            Piece pawn5 = new Pawn(0, 1, Game.Player2, this, "K2"); // B1
            Piece pawn6 = new Pawn(0, 2, Game.Player2, this, "K3"); // C1
        }

        /// <summary>
        /// Returns the name atribute of the Piece object in the specified position
        /// Used by Program.DrawBoard
        /// </summary>
        /// <param name="row">row index</param>
        /// <param name="column">column index</param>
        /// <returns></returns>
        public string this[int row, int column]
        {
            get
            {
                return BoardArray[row, column] == null ? "  " : BoardArray[row, column].Name;
            }
        }

    }
}
