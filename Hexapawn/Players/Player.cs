using Hexapawn.GameComponents;
using Hexapawn.Pieces;

namespace Hexapawn.Players
{
    public abstract class Player
    {
        public string Name { get; private set; } // Player nickname to be shown
        public Game Game { get; private set; } // Game in which this player is playing
        public Color Color { get; private set; } // Player color (Black or White)

        protected Player(Game game, Color color, string name)
        {
            Game = game;
            Color = color;
            Name = name;
        }

        /// <summary>
        /// Checks if the player still has pieces on board
        /// </summary>
        public bool HasPieces()
        {
            int pieceQuantity = 0;

            foreach (Piece piece in Game.Board.BoardArray)
            {
                if (piece == null)
                {
                    continue;
                }

                if (piece.Owner.Color == Color)
                {
                    pieceQuantity++;
                }
            }

            return pieceQuantity != 0;
        }
    }
}
