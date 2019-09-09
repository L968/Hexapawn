using Hexapawn.Exceptions;
using Hexapawn.GameComponents;
using Hexapawn.Players;

namespace Hexapawn.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(int xPosition, int yPosition, Player owner, Board board, string name) : base(xPosition, yPosition, owner, board, name)
        {
            
        }

        public override void IsValidMove(int[] positionIndexInBoardArray)
        {
            if (IsMovingForward(positionIndexInBoardArray[0], positionIndexInBoardArray[1]))
            {
                return;
            }

            if (IsCapturing(positionIndexInBoardArray[0], positionIndexInBoardArray[1]))
            {
                return;
            }

            throw new MoveException("Invalid Move");
        }

        public override bool CanMove()
        {
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    try
                    {
                        IsValidMove(new int[2] { i, j });
                        return true;
                    }
                    catch (MoveException)
                    {
                        continue;
                    }
                }
            }

            return false;
        }

        private bool IsMovingForward(int xDestinyPosition, int yDestinyPosition)
        {
            switch (Owner.Color)
            {
                case Color.WHITE:
                    // Checking if it's moving forward
                    if(XPositionOnBoard - 1 == xDestinyPosition
                    && YPositionOnBoard == yDestinyPosition)
                    {
                        // Checking if the destiny position has a piece
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
                    throw new MoveException("Piece color not found");
            }
        }

        private bool IsCapturing(int xDestinyPosition, int yDestinyPosition)
        {
            switch (Owner.Color)
            {
                case Color.WHITE:
                    if (XPositionOnBoard - 1 == xDestinyPosition
                    && (YPositionOnBoard + 1 == yDestinyPosition || YPositionOnBoard - 1 == yDestinyPosition))
                    {
                        // Checking if the destiny position has a piece
                        if (Owner.Game.Board[xDestinyPosition, yDestinyPosition] != null)
                        {
                            // Checking if the piece being captured is from the same team
                            if (Owner.Game.Board[xDestinyPosition, yDestinyPosition].Owner.Color != Color.WHITE)
                            {
                                return true;
                            }
                            else
                            {
                                throw new MoveException("You cannot capture your own piece");
                            }
                        }
                        else
                        {
                            throw new MoveException("There's no pieces to be captured in this position");
                        }
                    }
                    else
                    {
                        throw new MoveException("Invalid move");
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
                                throw new MoveException("You cannot capture your own piece");
                            }
                        }
                        else
                        {
                            throw new MoveException("There's no pieces to be captured in this position");
                        }
                    }
                    else
                    {
                        throw new MoveException("Invalid move");
                    }
                default:
                    throw new MoveException("Piece color not found");
            }
        }
  
    }
}