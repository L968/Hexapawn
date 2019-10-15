using System.Text;
using System.Data.SQLite;
using System.Data;
using System;

namespace Hexapawn.IA.DAL
{
    public class MoveDAL
    {
        private string BotName { get; set; }

        public MoveDAL(string botName)
        {
            BotName = botName;
        }

        public void CreateTable()
        {
            var query = new StringBuilder();
            query.Append("CREATE TABLE IF NOT EXISTS move (");
            query.Append("    move_id         INTEGER PRIMARY KEY AUTOINCREMENT,");
            query.Append("    turn            INTEGER NOT NULL,");
            query.Append("    board           TEXT,");
            query.Append("    move_piece      TEXT,");
            query.Append("    move_position   TEXT,");
            query.Append("    active          INTEGER DEFAULT 1");
            query.Append(")");

            using (var conn = HelperDAL.GetConnection(BotName))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = query.ToString();
                cmd.ExecuteNonQuery();
            }
        }

        public int? GetMoveId(string piece, string position, string boardString, int turn)
        {
            var dt = new DataTable();

            var query = new StringBuilder();
            query.Append("SELECT move_id ");
            query.Append("FROM move ");
            query.Append("WHERE board         = @BoardString ");
            query.Append("  AND move_piece    = @Piece ");
            query.Append("  AND move_position = @Position");
            query.Append("  AND turn          = @Turn");

            using (var conn = HelperDAL.GetConnection(BotName))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = query.ToString();
                cmd.Parameters.AddWithValue("@Piece", piece);
                cmd.Parameters.AddWithValue("@Position", position);
                cmd.Parameters.AddWithValue("@BoardString", boardString);
                cmd.Parameters.AddWithValue("@Turn", turn);

                var da = new SQLiteDataAdapter(cmd);
                da.Fill(dt);
            }

            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0]["move_id"]);
            }
        }

        public DataTable GetMovesByBoard(string boardString, int turn)
        {
            var dt = new DataTable();

            var query = new StringBuilder();
            query.Append("SELECT * ");
            query.Append("FROM move ");
            query.Append("WHERE board  = @BoardString ");
            query.Append("  AND turn   = @Turn ");

            using (var conn = HelperDAL.GetConnection(BotName))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = query.ToString();
                cmd.Parameters.AddWithValue("@BoardString", boardString);
                cmd.Parameters.AddWithValue("@Turn", turn);

                var da = new SQLiteDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }

        public void InsertNewMove(string[] move, string boardString, int turn)
        {
            var query = new StringBuilder();
            query.Append("INSERT INTO move (");
            query.Append("                  turn, ");
            query.Append("                  board, ");
            query.Append("                  move_piece, ");
            query.Append("                  move_position ");
            query.Append("                 ) ");
            query.Append("   VALUES (");
            query.Append("           @Turn, ");
            query.Append("           @Board, ");
            query.Append("           @Move_Piece, ");
            query.Append("           @Move_Position ");
            query.Append("          )");

            using (var conn = HelperDAL.GetConnection(BotName))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = query.ToString();
                cmd.Parameters.AddWithValue("@Turn", turn);
                cmd.Parameters.AddWithValue("@Board", boardString);
                cmd.Parameters.AddWithValue("@Move_Piece", move[0]);
                cmd.Parameters.AddWithValue("@Move_Position", move[1]);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// When the bot performs a move that would cause his lost, it will set that move chance of being picked to 0
        /// </summary>
        public void SetMoveActiveToZero(string[] move, string boardString, int turn)
        {
            var query = new StringBuilder();
            query.Append("UPDATE move ");
            query.Append("SET active = 0 ");
            query.Append("WHERE turn          = @Turn ");
            query.Append("  AND board         = @Board");
            query.Append("  AND move_piece    = @Move_piece");
            query.Append("  AND move_position = @Move_position");

            using (var conn = HelperDAL.GetConnection(BotName))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = query.ToString();
                cmd.Parameters.AddWithValue("@Turn", turn);
                cmd.Parameters.AddWithValue("@Board", boardString);
                cmd.Parameters.AddWithValue("@Move_piece", move[0]);
                cmd.Parameters.AddWithValue("@Move_position", move[1]);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
