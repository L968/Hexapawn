using System.Data.SQLite;

namespace Hexapawn.IA.DAL
{
    public class HelperDAL
    {

        public static SQLiteConnection GetConnection(string botName)
        {
            var conn = new SQLiteConnection($@"Data Source={Helper.DataBaseFolderPath}\{botName}Moves.db3; Version=3;");
            conn.Open();
            return conn;
        }

        public static void CreateDataBaseFile(string botName)
        {
            SQLiteConnection.CreateFile(Helper.DataBaseFolderPath + $@"\{botName}Moves.db3");
        }

        public static void CreateIATables(string botName)
        {
            var moveDal = new MoveDAL(botName);
            moveDal.CreateTable();
        }

    }
}
