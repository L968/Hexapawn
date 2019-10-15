using System.Data.SQLite;
using System.IO;
using Hexapawn.IA.DAL;

namespace Hexapawn.IA.BLL
{
    class HelperBLL
    {
        public static SQLiteConnection GetConnection(string botName)
        {
            return HelperDAL.GetConnection(botName);
        }

        /// <summary>
        /// Creates the .db3 file unless it already exists
        /// </summary>
        /// <param name="botName">The file will be created using the bot's name, so every bot has it's own intelligence</param>
        public static void CreateDataBaseFile(string botName)
        {
            if (!File.Exists(Helper.DataBaseFolderPath + $@"\{botName}Moves.db3"))
            {
                HelperDAL.CreateDataBaseFile(botName);
            }
        }

        public static void CreateIATables(string botName)
        {
            HelperDAL.CreateIATables(botName);
        }
    }
}
