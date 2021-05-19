using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Game_2
{
    public static class GameController
    {
        private static Image sheet = new Bitmap(Path
            .Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName,
                "Sprites\\Ground-Sheet.png"));
// GGGGGGGGGGGGGGGGGGGGGGGG <= 20 стенок
// EEEEEEEEEEEEEEEEEEEEEEEE <= 20 пустых клеток

        private const string Level1 = @"
GGGGGGGGGGGGGGGGGGGGGGGG
GEEEEEEEEEEEEEEEEEEEEEEG
GEEEEEEEEEEEEEEEEEEEEEEG
GEEEGGGGGGGGEEEEEEEEEEEG
GEEEGEEEEEEGEEEEEEEEEEEG
GEEEGEEEEEEGEEEEEEEEEEEG
GEEEGEEEEGGGEEEEEEEEEEEG
GEEEGEEEEGEEEEEEEEEEEEEG
GEEEGGEEGGEEEEEEEEEEEEEG
GEEEEEEEEEEEEEEEEEEEEEEG
GEEEEEEEEEEEEEEEEEEEEEEG
GEEEEEEEEEEEEEEEEEEEEEEG
GEEEEEEEEEEEEEEEEEEEEEEG
GEEEEEEEEEEEEEEEEEEEEEEG
GEEEEEEEEEEEEEEEEEEEEEEG
GEEEEEEEEEEEEEEEEEEEEEEG
GEEEEEEEEEEEEEEEEEEEEEEG
GEEEEEEEEEEEEEEEEEEEEEEG
GEEEEEEEEEEEEEEEEEEEEEEG
GGGGGGGGGGGGGGGGGGGGGGGG";


        public static readonly Cell[,] Map = CreateMap(Level1);
        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);

        #region MapCreation
        private static Cell[,] CreateMap(string map, string separator = "\r\n")
        {
            var rows = map
                .Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            var result = new Cell[rows[0].Length, rows.Length];
            for (var x = 0; x < rows[0].Length; x++)
            for (var y = 0; y < rows.Length; y++)
                result[x, y] = CreateCellBySymbol(x * 32, y * 32, rows[y][x]);
            return result;
        }

        private static Cell CreateCellBySymbol(int x, int y, char c)
        {
            switch (c)
            {
                case 'G':
                    return new Cell(x, y, true);
                case 'E':
                    return new Cell(x, y, false);
                default:
                    throw new Exception($"wrong character for Cell {c}");
            }
        }

        

        #endregion

        public static void DrawMap(Graphics g)
        {
            foreach (var cell in Map)
            {
                // g.FillRectangle(cell.HaveCollision is true ? Brushes.Brown : Brushes.Wheat,
                //     cell.Rect);
                g.DrawImage(sheet, new Rectangle(new Point(cell.X, cell.Y), new Size(32, 32)),
                    cell.HaveCollision ? 32 : 0, 0, 32, 32, GraphicsUnit.Pixel);
            }
        }
    }
}