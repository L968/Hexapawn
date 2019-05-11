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

        protected override void IsValidPath(int xDestinyPosition, int yDestinyPosition)
        {
            if (PawnCanMoveForward(xDestinyPosition, yDestinyPosition))
            {
                return;
            }

            if (PawnCanCapture(xDestinyPosition, yDestinyPosition))
            {

            }

            throw new MoveException("Movimento inválido");
        }

        private bool PawnCanMoveForward(int xDestinyPosition, int yDestinyPosition)
        {
            switch (Owner.Color)
            {
                case Color.WHITE:
                    // Checking if it's moving forward
                    if(XPositionOnBoard - 1 == xDestinyPosition && YPositionOnBoard == yDestinyPosition)
                    {
                        // Checking if the destiny position has a pawn in front of this pawn
                        return Owner.Game.Board[xDestinyPosition, yDestinyPosition] == null;
                    }
                    else
                    {
                        return false;
                    }
                case Color.BLACK:
                    if (XPositionOnBoard + 1 == xDestinyPosition && YPositionOnBoard == yDestinyPosition)
                    {
                        return Owner.Game.Board[xDestinyPosition, yDestinyPosition] == null;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    throw new MoveException("Cor da peça não encontrada");
            }
        }

        private bool PawnCanCapture(int xDestinyPosition, int yDestinyPosition)
        {
            switch (Owner.Color)
            {
                case Color.WHITE:
                    break;
                case Color.BLACK:
                    break;
                default:
                    break;
            }
        }

    }
}
