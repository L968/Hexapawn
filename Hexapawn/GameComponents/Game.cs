using Hexapawn.Exceptions;
using Hexapawn.Pieces;
using Hexapawn.Players;

namespace Hexapawn.GameComponents
{
    public class Game
    {
        public Board Board { get; private set; }
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public Player Winner { get; private set; }
        public Player ActivePlayer { get; private set; }
        public Player InactivePlayer { get; private set; }
        public int Turn { get; private set; }

        public Game()
        {
            Turn = 1;
            Player1 = new Human(this, Color.WHITE, "Human");
            Player2 = new Bot(this, Color.BLACK, "Bot");
            Board = new Board(this);
            ActivePlayer = Player1;
            InactivePlayer = Player2;
        }

        public void Move(string pieceName, string positionName)
        {
            var piece = GetPieceByName(pieceName);
            var positionIndexInBoardArray = Helper.GetPositionIndexInBoardArray(positionName);

            IsActivePlayerThePieceOwner(piece);
            piece.IsValidMove(positionIndexInBoardArray);

            if (!string.IsNullOrEmpty(piece.InvalidMoveMessage))
            {
                throw new MoveException(piece.InvalidMoveMessage);
            }

            MovePiece(piece, positionIndexInBoardArray);
            CheckWinner();
            SwitchActivePlayer();
            Turn++;
        }

        public void Move(string[] move)
        {
            var piece = GetPieceByName(move[0]);
            var positionIndexInBoardArray = Helper.GetPositionIndexInBoardArray(move[1]);

            IsActivePlayerThePieceOwner(piece);
            piece.IsValidMove(positionIndexInBoardArray);

            if (!string.IsNullOrEmpty(piece.InvalidMoveMessage))
            {
                throw new MoveException(piece.InvalidMoveMessage);
            }

            MovePiece(piece, positionIndexInBoardArray);
            CheckWinner();
            SwitchActivePlayer();
            Turn++;
        }

        /// <summary>
        /// Checking if the Active Player is the Piece Owner
        /// </summary>
        /// <param name="piece">The piece to be checked</param>
        private void IsActivePlayerThePieceOwner(Piece piece)
        {
            if (piece.Owner == ActivePlayer)
            {
                return;
            }
            else
            {
                throw new MoveException("You are not the owner of this piece");
            }
        }

        /// <summary>
        /// Returns the Piece object searching for the piece's name
        /// </summary>
        /// <param name="pieceName">Piece name</param>
        /// <returns></returns>
        private Piece GetPieceByName(string pieceName)
        {
            foreach (var piece in Board.BoardArray)
            {
                if (piece == null)
                {
                    continue;
                }

                if (pieceName == piece.Name)
                {
                    return Board.BoardArray[piece.XPositionOnBoard, piece.YPositionOnBoard];
                }
            }
            throw new MoveException("Piece not found");
        }

        private void MovePiece(Piece piece, int[] positionIndexInBoardArray)
        {
            Board.BoardArray[piece.XPositionOnBoard, piece.YPositionOnBoard] = null;
            piece.XPositionOnBoard = positionIndexInBoardArray[0];
            piece.YPositionOnBoard = positionIndexInBoardArray[1];
            Board.BoardArray[piece.XPositionOnBoard, piece.YPositionOnBoard] = piece;
        }

        private void SwitchActivePlayer()
        {
            if (ActivePlayer == Player1)
            {
                ActivePlayer = Player2;
                InactivePlayer = Player1;
            }
            else if (ActivePlayer == Player2)
            {
                ActivePlayer = Player1;
                InactivePlayer = Player2;
            }
        }

        private void CheckWinner()
        {
            #region Win Condition 1 - Player doesn't have any pieces left
            if (!InactivePlayer.HasPieces())
            {
                Winner = ActivePlayer;
                return;
            }
            #endregion

            #region Win Condition 2 - Cross the board
            switch (ActivePlayer.Color)
            {
                case Color.WHITE:
                    foreach (var piece in Board.BoardArray)
                    {
                        if (piece == null)
                        {
                            continue;
                        }

                        if (piece.Owner.Color == Color.WHITE)
                        {
                            if (piece.XPositionOnBoard == 0)
                            {
                                Winner = ActivePlayer;
                                return;
                            }
                        }
                    }
                    break;
                case Color.BLACK:
                    foreach (var piece in Board.BoardArray)
                    {
                        if (piece == null)
                        {
                            continue;
                        }

                        if (piece.Owner.Color == Color.BLACK)
                        {
                            if (piece.XPositionOnBoard == 2)
                            {
                                Winner = ActivePlayer;
                                return;
                            }
                        }
                    }
                    break;
            }
            #endregion

            #region Win Condition 3 - Next player doesn't have any moves
            foreach (var piece in Board.BoardArray)
            {
                if (piece == null)
                {
                    continue;
                }

                if (piece.Owner.Color == InactivePlayer.Color)
                {
                    if (piece.GetValidPositionsToMove().Count != 0)
                    {
                        return;
                    }
                }
            }
            Winner = ActivePlayer;
            #endregion
        }

    }
}