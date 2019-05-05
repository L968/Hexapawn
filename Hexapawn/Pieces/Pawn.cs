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
        public Pawn(int xPosition, int yPosition, Player owner)
        {
            XPositionOnBoard = xPosition;
            YPositionOnBoard = yPosition;
            Owner = owner;
        }
    }
}
