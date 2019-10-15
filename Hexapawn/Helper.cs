using Hexapawn.Exceptions;
using System;
using System.IO;

namespace Hexapawn
{
    public static class Helper
    {
        public static readonly string DataBaseFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Hexapawn";

        public static void CreateApplicationFolder()
        {
            Directory.CreateDirectory(DataBaseFolderPath);
        }

        /// <summary>
        /// </summary>
        /// <param name="tile">Position name e.g. A1, B2</param>
        /// <returns>A int[] containing the XPositionOnBoardArray and YPositionOnBoardArray</returns>
        public static int[] GetPositionIndexInBoardArray(string tile)
        {
            switch (tile)
            {
                case "A1": return new int[2] { 0, 0 };
                case "A2": return new int[2] { 1, 0 };
                case "A3": return new int[2] { 2, 0 };
                case "B1": return new int[2] { 0, 1 };
                case "B2": return new int[2] { 1, 1 };
                case "B3": return new int[2] { 2, 1 };
                case "C1": return new int[2] { 0, 2 };
                case "C2": return new int[2] { 1, 2 };
                case "C3": return new int[2] { 2, 2 };
                default: throw new MoveException("Board position not found");
            }
        }

        public static string GetPositionNameByIndex(int[] index)
        {
            var stringIndex = string.Join("", index);

            switch (stringIndex)
            {
                case "00": return "A1";
                case "10": return "A2";
                case "20": return "A3";
                case "01": return "B1";
                case "11": return "B2";
                case "21": return "B3";
                case "02": return "C1";
                case "12": return "C2";
                case "22": return "C3";
                default: throw new MoveException("Board index not found");
            }
        }

    }
}
