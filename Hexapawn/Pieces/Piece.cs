using Hexapawn.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapawn.Pieces
{
    public abstract class Piece
    {
        public string Name { get; set; }
        public int XPositionOnBoard { get; set; }
        public int YPositionOnBoard { get; set; }

        public Player Owner { get; set; }
    }
}
