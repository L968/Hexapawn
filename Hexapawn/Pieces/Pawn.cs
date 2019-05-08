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
    }
}
