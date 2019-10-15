using Hexapawn.GameComponents;
using Hexapawn.Pieces;
using System;
using System.Collections.Generic;
using Hexapawn.IA.BLL;
using System.Data;

namespace Hexapawn.Players
{
    public class Bot : Player
    {
        private List<Piece> BotPieces { get; set; }
        private List<string[]> MovesLog { get; set; }
        private List<string> BoardLog { get; set; }
        private List<int> TurnLog { get; set; }

        Random random = new Random();

        public Bot(Game game, Color color, string name) : base(game, color, name)
        {
            BotPieces = new List<Piece>();
            MovesLog = new List<string[]>();
            BoardLog = new List<string>();
            TurnLog = new List<int>();
            SetupDatabase();
        }

        /// <summary>
        /// Generates a move from the Bot's database
        /// </summary>
        /// <returns>A array containing the Piece name to be moved and the Board position name</returns>
        public string[] GenerateMove()
        {
            var moveBll = new MoveBLL(Name);
            var moves = new DataTable();
            var move = new string[2];

            var numberOfPossibleValidMoves = GetNumberOfBotPossibleValidMoves();

            while (true)
            {
                moves = moveBll.GetMovesByBoard(Game.Board.ToString(), Game.Turn);

                if (moves.Rows.Count < numberOfPossibleValidMoves)
                {
                    InsertNewMoves();
                    continue;
                }

                break;
            }

            var activeMovesArray = moves.Select("active = 1");
            var dtActiveMoves = new DataTable();

            if (activeMovesArray.Length == 0)
            {
                dtActiveMoves = moves;

                if (MovesLog.Count != 0)
                {
                    SetMoveActiveToZero(MovesLog[MovesLog.Count - 1], BoardLog[BoardLog.Count - 1], TurnLog[BoardLog.Count - 1]);
                }
            }
            else
            {
                dtActiveMoves = activeMovesArray.CopyToDataTable();
            }

            int randomRowIndex = random.Next(dtActiveMoves.Rows.Count);
            move[0] = (string)dtActiveMoves.Rows[randomRowIndex]["move_piece"];
            move[1] = (string)dtActiveMoves.Rows[randomRowIndex]["move_position"];

            MovesLog.Add(move);
            BoardLog.Add(Game.Board.ToString());
            TurnLog.Add(Game.Turn);

            return move;
        }

        public void SetLastMoveActiveToZero()
        {
            var moveBll = new MoveBLL(Name);
            moveBll.SetMoveActiveToZero(MovesLog[MovesLog.Count-1], BoardLog[BoardLog.Count-1], TurnLog[BoardLog.Count-1]);
        }

        private void SetMoveActiveToZero(string[] move, string board, int turn)
        {
            var moveBll = new MoveBLL(Name);
            moveBll.SetMoveActiveToZero(move, board, turn);
        }

        private int GetNumberOfBotPossibleValidMoves()
        {
            UpdateBotPiecesList();
            int sum = 0;

            foreach (Pawn piece in BotPieces)
            {
                sum += piece.GetValidPositionsToMove().Count;
            }

            return sum;
        }

        private void InsertNewMoves()
        {
            UpdateBotPiecesList();
            var board = Game.Board.ToString();
            var move = new string[2];

            foreach(var piece in BotPieces)
            {
                move[0] = piece.Name;

                List<int[]> validPositionsToMove = piece.GetValidPositionsToMove();

                if (validPositionsToMove.Count == 0)
                {
                    continue;
                }
                else
                {
                    foreach (int[] validPosition in validPositionsToMove)
                    {
                        move[1] = Helper.GetPositionNameByIndex(validPosition);

                        var moveBll = new MoveBLL(Name);
                        moveBll.InsertNewMove(move, board, Game.Turn);
                    }
                }
            }
        }

        /// <summary>
        /// Populates the BotPieces List with all bot pieces in the actual state of the game
        /// </summary>
        private void UpdateBotPiecesList()
        {
            BotPieces.Clear();
            foreach (var piece in Game.Board.BoardArray)
            {
                if (piece == null)
                {
                    continue;
                }

                if (piece.Owner == this)
                {
                    BotPieces.Add(piece);
                }
            }
        }

        private void SetupDatabase()
        {
            Helper.CreateApplicationFolder();
            HelperBLL.CreateDataBaseFile(Name);
            HelperBLL.CreateIATables(Name);
        }

    }
}