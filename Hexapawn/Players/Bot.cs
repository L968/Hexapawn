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
        private List<string> Pieces { get; set; }

        public Bot(Game game, Color color, string name) : base(game, color, name)
        {
            Pieces = new List<string>();
            Random = new Random();
        }

        public string[] GetMove()
        {
            GetPieces();

            string[] output = new string[2];

            RandomSelectedIndex = Random.Next(Pieces.Count);
            output[0] = Pieces[RandomSelectedIndex];

            RandomSelectedIndex = Random.Next(Game.Board.BoardArray.Length);
            output[1] = GetPositionNameByIndex(RandomSelectedIndex);

            Pieces.Clear();
            return output;

        }

        /// <summary>
        /// Populates the Pieces List reading all pieces on board
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
                    Pieces.Add(item.Name);
                }
            }
        }

        /// <summary>
        /// Returns the position name from the board in all 3x3 possible positions
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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
    }
}
