using Hexapawn.Pieces;

namespace Hexapawn.GameComponents
{
    public class Board
    {
        public Piece[,] BoardArray { get; set; } // A array of pieces representing the board
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

            //Setting Player2 pieces
            var pawn4 = new Pawn(0, 0, Game.Player2, this, "K1"); // A1
            var pawn5 = new Pawn(0, 1, Game.Player2, this, "K2"); // B1
            var pawn6 = new Pawn(0, 2, Game.Player2, this, "K3"); // C1

            //Setting Player1 pieces
            var pawn1 = new Pawn(2, 0, Game.Player1, this, "P1"); // A3
            var pawn2 = new Pawn(2, 1, Game.Player1, this, "P2"); // B3
            var pawn3 = new Pawn(2, 2, Game.Player1, this, "P3"); // C3
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
        /// Converts the board to a K1K2K3______P1P2P3 format
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var boardString = "";

            foreach (var piece in BoardArray)
            {
                if (piece == null)
                {
                    boardString += "__";
                    continue;
                }

                boardString += piece.Name;
            }

            return boardString;
        }

    }
}
