using System.Collections.Generic;
using Hexapawn.GameComponents;
using Hexapawn.Players;

namespace Hexapawn.Pieces
{
    public abstract class Piece
    {
        public string Name { get; private set; }
        public Player Owner { get; private set; }
        public int XPositionOnBoard { get; set; }
        public int YPositionOnBoard { get; set; }
        public string InvalidMoveMessage { get; protected set; }

        protected Piece(int xPosition, int yPosition, Player owner, Board board, string name)
        {
            XPositionOnBoard = xPosition;
            YPositionOnBoard = yPosition;
            Owner = owner;
            Name = name;

            board.BoardArray[XPositionOnBoard, YPositionOnBoard] = this; // Self allocating on the board
        }

        public abstract bool IsValidMove(int[] positionIndexInBoardArray);
        public abstract List<int[]> GetValidPositionsToMove();
    }
}
