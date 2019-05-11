using Hexapawn.Exceptions;
using Hexapawn.GameComponents;
using Hexapawn.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapawn.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(int xPosition, int yPosition, Player owner, Board board, string name) : base(xPosition, yPosition, owner, board, name)
        {
            
        }

        protected override void IsValidPath(int xPosition, int yPosition)
        {
            if (IsPawnMovingForward(xPosition, yPosition))
            {

            }  
        }

        private bool IsPawnMovingForward(int xPosition, int yPosition)
        {
            switch (Owner.Color)
            {
                case Color.WHITE:
                    return XPositionOnBoard - 1 == xPosition && YPositionOnBoard == yPosition;
                case Color.BLACK:
                    return XPositionOnBoard + 1 == xPosition && YPositionOnBoard == yPosition;
                default:
                    throw new MoveException("Cor da peça não encontrada");
            }
            
        }
    }
}
