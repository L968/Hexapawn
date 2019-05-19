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
        /// Board indexer to return or set the object Piece in the specified position
        /// </summary>
        /// <param name="row">Row number in BoadArray</param>
        /// <param name="column">Column number in BoardArray</param>
        /// <returns></returns>
        public Piece this[int row, int column]
        {
            get
            {
                return BoardArray[row, column];
            }
            set
            {
                BoardArray[row, column] = value;
            }
        }

        /// <summary>
        /// Returns the Piece.Name string on the specified BoardArray position. If the object is null, it returns "  "
        /// </summary>
        /// <param name="row">Row number in BoadArray</param>
        /// <param name="column">>Column number in BoardArray</param>
        /// <returns></returns>
        public string GetPieceNameOnBoard(int row, int column)
        {
            return BoardArray[row, column] == null ? "  " : BoardArray[row, column].Name;
        }

        /// <summary>
        /// Converts the board array into a string to be used in the move json file
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            string output = "";

            foreach (Piece item in BoardArray)
            {
                if (item == null)
                {
                    output += "E";
                    continue;
                }

                switch (item.Owner.Color)
                {
                    case Color.WHITE:
                        output += "W";
                        break;
                    case Color.BLACK:
                        output += "B";
                        break;
                }
            }

            if (output.Length != 9)
            {
                throw new Exception("Erro ao transformar tabuleiro em Json\n");
            }

            return output;
        }

    }
}
