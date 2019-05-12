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

        public override bool IsValidPath(int xDestinyPosition, int yDestinyPosition)
        {
            if (PawnCanMoveForward(xDestinyPosition, yDestinyPosition))
            {
                return true;
            }

            if (PawnCanCapture(xDestinyPosition, yDestinyPosition))
            {
                return true;
            }

            throw new MoveException("Movimento inválido");
        }

        private bool PawnCanMoveForward(int xDestinyPosition, int yDestinyPosition)
        {
            switch (Owner.Color)
            {
                case Color.WHITE:
                    // Checking if it's moving forward
                    if(XPositionOnBoard - 1 == xDestinyPosition
                    && YPositionOnBoard == yDestinyPosition)
                    {
                        // Checking if the destiny position has a pawn in front of this pawn
                        return Owner.Game.Board[xDestinyPosition, yDestinyPosition] == null;
                    }
                    else
                    {
                        return false;
                    }
                case Color.BLACK:
                    if (XPositionOnBoard + 1 == xDestinyPosition
                     && YPositionOnBoard == yDestinyPosition)
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
                    // Cheking if it's making a capturing move (Diagonal move)
                    if (XPositionOnBoard - 1 == xDestinyPosition
                     && (YPositionOnBoard + 1 == yDestinyPosition || YPositionOnBoard - 1 == yDestinyPosition))
                    {
                        // Checking if the destiny position has a pawn
                        if (Owner.Game.Board[xDestinyPosition, yDestinyPosition] != null)
                        {
                            // Checking if the pawn being captured is from the same team
                            if (Owner.Game.Board[xDestinyPosition, yDestinyPosition].Owner.Color != Color.WHITE)
                            {
                                return true;
                            }
                            else
                            {
                                throw new MoveException("Você não pode capturar sua própria peça");
                            }
                        }
                        else
                        {
                            throw new MoveException("Não há peças para capturar nesta posição");
                        }
                    }
                    else
                    {
                        throw new MoveException("Não é possível capturar peças nessa posição");
                    }
                case Color.BLACK:
                    if (XPositionOnBoard + 1 == xDestinyPosition
                     && (YPositionOnBoard + 1 == yDestinyPosition || YPositionOnBoard - 1 == yDestinyPosition))
                    {
                        if (Owner.Game.Board[xDestinyPosition, yDestinyPosition] != null)
                        {
                            if (Owner.Game.Board[xDestinyPosition, yDestinyPosition].Owner.Color != Color.BLACK)
                            {
                                return true;
                            }
                            else
                            {
                                throw new MoveException("Você não pode capturar sua própria peça");
                            }
                        }
                        else
                        {
                            throw new MoveException("Não há peças para capturar nesta posição");
                        }
                    }
                    else
                    {
                        throw new MoveException("Não é possível capturar peças nessa posição");
                    }
                default:
                    throw new MoveException("Cor da peça não encontrada");
            }
        }

    }
}
