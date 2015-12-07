using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space_Invaders
{
    class MapRow
{
    public List<MapCell> Columns = new List<MapCell>();
}

    class TileMap
    {
        public List<MapRow> Rows = new List<MapRow>();
        public int MapWidth = 600;
        public int MapHeight = 650;

           public TileMap()
        {
            for (int y = 0; y < MapHeight; y++)
            {
                MapRow thisRow = new MapRow();
                for (int x = 0; x < MapWidth; x++)
                {
                    thisRow.Columns.Add(new MapCell(0));
                }
                Rows.Add(thisRow);
            }
            // Create Sample Map Data
           Rows[0].Columns[0].TileID = 2;
           Rows[0].Columns[1].TileID = 2;
           Rows[0].Columns[2].TileID = 2;
           Rows[0].Columns[3].TileID = 2;
           Rows[0].Columns[4].TileID = 2;
           Rows[0].Columns[5].TileID = 2;
           Rows[0].Columns[5].TileID = 2;

           Rows[1].Columns[3].TileID = 3;
           Rows[1].Columns[4].TileID = 1;
           Rows[1].Columns[5].TileID = 1;
           Rows[1].Columns[6].TileID = 1;
           Rows[1].Columns[7].TileID = 1;
           Rows[1].Columns[0].TileID = 2;
           // End Create Sample Map Data
        }
    }
}
