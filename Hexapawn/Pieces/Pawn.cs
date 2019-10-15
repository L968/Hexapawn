using Hexapawn.Exceptions;
using Hexapawn.GameComponents;
using Hexapawn.Players;
using System;
using System.Collections.Generic;

namespace Hexapawn.Pieces
{
    public class Pawn : Piece
    {

        private List<int[]> PossiblePositionsToMove { get; set; }

        public Pawn(int xPosition, int yPosition, Player owner, Board board, string name) : base(xPosition, yPosition, owner, board, name)
        {
            PossiblePositionsToMove = new List<int[]>();
        }

        public override bool IsValidMove(int[] positionIndexInBoardArray)
        {
            InvalidMoveMessage = null;

            if( IsMovingForwardPossible(positionIndexInBoardArray[0], positionIndexInBoardArray[1])
             || IsCapturingPossible(positionIndexInBoardArray[0], positionIndexInBoardArray[1]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override List<int[]> GetValidPositionsToMove()
        {
            UpdatePossiblePositionsToMoves();
            var validPositionsToMove = new List<int[]>();

            foreach (var move in PossiblePositionsToMove)
            {
                try
                {
                    if (IsValidMove(move))
                    {
                        validPositionsToMove.Add(move);
                    }
                }
                catch (MoveException)
                {
                    continue;
                }
            }

            return validPositionsToMove;
        }

        private void UpdatePossiblePositionsToMoves()
        {
            var moveForward = new int[2];
            var captureRight = new int[2];
            var captureLeft = new int[2];

            switch (Owner.Color)
            {
                case Color.WHITE:
                    moveForward[0] = XPositionOnBoard - 1;
                    moveForward[1] = YPositionOnBoard;

                    captureRight[0] = XPositionOnBoard - 1;
                    captureRight[1] = YPositionOnBoard + 1;

                    captureLeft[0] = XPositionOnBoard - 1;
                    captureLeft[1] = YPositionOnBoard - 1;
                    break;
                case Color.BLACK:
                    moveForward[0] = XPositionOnBoard + 1;
                    moveForward[1] = YPositionOnBoard;

                    captureRight[0] = XPositionOnBoard + 1;
                    captureRight[1] = YPositionOnBoard + 1;

                    captureLeft[0] = XPositionOnBoard + 1;
                    captureLeft[1] = YPositionOnBoard - 1;
                    break;
                default:
                    throw new ApplicationException("Piece color not found");
            }

            PossiblePositionsToMove.Clear();
            PossiblePositionsToMove.Add(moveForward);
            PossiblePositionsToMove.Add(captureRight);
            PossiblePositionsToMove.Add(captureLeft);
        }

        private bool IsMovingForwardPossible(int xDestinyPosition, int yDestinyPosition)
        {
            switch (Owner.Color)
            {
                case Color.WHITE:
                    // Checking if it's moving forward
                    if(XPositionOnBoard - 1 == xDestinyPosition
                    && YPositionOnBoard     == yDestinyPosition)
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
                     && YPositionOnBoard     == yDestinyPosition)
                    {
                        return Owner.Game.Board[xDestinyPosition, yDestinyPosition] == null;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    throw new ApplicationException("Piece color not found");
            }
        }

        private bool IsCapturingPossible(int xDestinyPosition, int yDestinyPosition)
        {
            try
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
                                    InvalidMoveMessage = "You cannot capture your own piece";
                                    return false;
                                }
                            }
                            else
                            {
                                InvalidMoveMessage = "There's no pieces to be captured in this position";
                                return false;
                            }
                        }
                        else
                        {
                            InvalidMoveMessage = "Invalid move";
                            return false;
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
                                    InvalidMoveMessage = "You cannot capture your own piece";
                                    return false;
                                }
                            }
                            else
                            {
                                InvalidMoveMessage = "There's no pieces to be captured in this position";
                                return false;
                            }
                        }
                        else
                        {
                            InvalidMoveMessage = "Invalid move";
                            return false;
                        }
                    default:
                        throw new ApplicationException("Piece color not found");
                }
            }
            catch (IndexOutOfRangeException)
            {
                InvalidMoveMessage = "Invalid move";
                return false;
            }
        }

    }
}