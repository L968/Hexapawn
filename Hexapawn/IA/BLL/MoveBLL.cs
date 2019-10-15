using System.Data;
using Hexapawn.IA.DAL;

namespace Hexapawn.IA.BLL
{
    public class MoveBLL
    {
        private string BotName { get; set; }

        public MoveBLL(string botName)
        {
            BotName = botName;
        }

        public void CreateTable()
        {
            var moveDal = new MoveDAL(BotName);
            moveDal.CreateTable();
        }

        public DataTable GetMovesByBoard(string boardString, int turn)
        {
            var moveDal = new MoveDAL(BotName);
            return moveDal.GetMovesByBoard(boardString, turn);
        }

        public int? GetMoveId(string piece, string position, string boardString, int turn)
        {
            var moveDal = new MoveDAL(BotName);
            return moveDal.GetMoveId(piece, position, boardString, turn);
        }

        public void InsertNewMove(string[] move, string boardString, int turn)
        {
            var moveDal = new MoveDAL(BotName);
            var moveId = moveDal.GetMoveId(move[0], move[1], boardString, turn);

            if (moveId != null)
            {
                return;
            }
            else
            {
                moveDal.InsertNewMove(move, boardString, turn);
            }
        }

        /// <summary>
        /// When the bot performs a move that would cause his lost, it will set that move chance of being picked to 0
        /// </summary>
        public void SetMoveActiveToZero(string[] move, string boardString, int turn)
        {
            var moveDal = new MoveDAL(BotName);
            moveDal.SetMoveActiveToZero(move, boardString, turn);
        }

    }
}
