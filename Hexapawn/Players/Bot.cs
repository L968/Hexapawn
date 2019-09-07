using Hexapawn.GameComponents;
using Hexapawn.Pieces;
using System;
using System.Collections.Generic;

namespace Hexapawn.Players
{
    public class Bot : Player
    {
        private List<Piece> BotPieces { get; set; }

        public Bot(Game game, Color color, string name) : base(game, color, name)
        {
            BotPieces = new List<Piece>();
        }

        /// <summary>
        /// Generates the move from it's move database
        /// </summary>
        /// <returns>A array containing the Piece name to be moved and the Board position name</returns>
        public string[] GetMove()
        {
            var random = new Random();
            FillBotPiecesList();

            string[] move = new string[2];

            int randomSelectedIndex = random.Next(BotPieces.Count);
            move[0] = BotPieces[randomSelectedIndex].Name;

            randomSelectedIndex = random.Next(Game.Board.BoardArray.Length);
            move[1] = GetBoardPositionNameByIndex(randomSelectedIndex);

            BotPieces.Clear(); 
            return move;

        }

        /// <summary>
        /// Populates the BotPieces List with all bot pieces in the actual state of the game
        /// </summary>
        private void FillBotPiecesList()
        {
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

        /// <summary>
        /// Returns the position name from the board in all 3x3 possible positions
        /// </summary>
        /// <param name="index">The board index</param>
        private string GetBoardPositionNameByIndex(int index)
        {
            switch (index)
            {
                case 0: return "A1";
                case 1: return "B1";
                case 2: return "C1";
                case 3: return "A2";
                case 4: return "B2";
                case 5: return "C2";
                case 6: return "A3";
                case 7: return "B3";
                case 8: return "C3";
                default:
                    throw new IndexOutOfRangeException("Invalid Index");
            }
        }
       
        #region Move's Storage
        /// <summary>
        /// When the bot performs a unlearned move, it will it to the database
        /// </summary>
        /// <param name="move"></param>
        private void AddMoveToDatabase(string[] move)
        {

        }

        /// <summary>
        /// When the bot performs a move that would cause his lost, it will set the move chance to 0
        /// </summary>
        private void SetMoveChanceToZero()
        {

        }
        #endregion
    }
}
