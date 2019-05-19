using Hexapawn.GameComponents;
using Hexapawn.Pieces;
using Hexapawn.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapawn.Players
{
    public class Bot : Player
    {
        // Getting all pieces
        private Random Random { get; set; }
        private int RandomSelectedIndex { get; set; }
        private List<string> BotPieces { get; set; } // List containing all bot pieces in the current state of the game

        public Bot(Game game, Color color, string name) : base(game, color, name)
        {
            BotPieces = new List<string>();
            Random = new Random();
        }

        /// <summary>
        /// Generates the move from it's json file
        /// </summary>
        /// <returns></returns>
        public string[] GetMove()
        {
            GetPieces();

            string[] output = new string[2];

            RandomSelectedIndex = Random.Next(BotPieces.Count);
            output[0] = BotPieces[RandomSelectedIndex];

            RandomSelectedIndex = Random.Next(Game.Board.BoardArray.Length);
            output[1] = GetPositionNameByIndex(RandomSelectedIndex);

            BotPieces.Clear(); 
            return output;

        }

        /// <summary>
        /// Populates the BotPieces List with all bot pieces in the actual state of the game
        /// </summary>
        private void GetPieces()
        {
            foreach (Piece item in Game.Board.BoardArray)
            {
                if (item == null)
                {
                    continue;
                }

                if (item.Owner == this)
                {
                    BotPieces.Add(item.Name);
                }
            }
        }

        /// <summary>
        /// Returns the position name from the board in all 3x3 possible positions
        /// </summary>
        /// <param name="index"></param>
        private string GetPositionNameByIndex(int index)
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
                    throw new IndexOutOfRangeException("Indice inválido");
            }
        }

        /// <summary>
        /// When the bot performs a unlearned move, it will add to his move json file
        /// </summary>
        /// <param name="move"></param>
        private void AddMoveToJsonFile(string[] move)
        {

        }

        /// <summary>
        /// When the bot performs a move that would cause his lost, it will set the move chance to 0
        /// </summary>
        private void SetMoveChanceToZero()
        {

        }
    }
}
