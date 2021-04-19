using System;
using CarteAuTresor.Interface;

namespace CarteAuTresor
{
    public class Map : IMap
    {

        public ITile[,] TileMap { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public Map(int sizeX, int sizeY)
        {
            if (sizeX < 0 && sizeX < 0)
                throw new Exception("The map size need to be bigger than 0");
            this.SizeX = sizeX;
            this.SizeY = sizeY;
            TileMap = new ITile[sizeY, sizeX];
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    TileMap[y, x] = new Plain(x, y);
                }
            }
        }

        public void AddMountainToMap(Mountain mountain)
        {
            if (!CheckIffOutOfRange(mountain.PosY, mountain.PosX)
                && TileMap[mountain.PosY, mountain.PosX].tileType == TileType.PLAIN)
                TileMap[mountain.PosY, mountain.PosX] = mountain;
            else
                throw new Exception($"Cannot add {TileType.MOUNTAIN} tile at position x: {mountain.PosX} y: {mountain.PosY} is out of range or already has a incompatible tile.");

        }

        public void AddTreasureToMap(Treasure treasure)
        {
            if (!CheckIffOutOfRange(treasure.PosY, treasure.PosX)
                && TileMap[treasure.PosY, treasure.PosX].tileType != TileType.MOUNTAIN)
            {
                if (TileMap[treasure.PosY, treasure.PosX].tileType == TileType.TREASURE)
                {
                    var tempTreasure = (Treasure)TileMap[treasure.PosY, treasure.PosX];
                    tempTreasure.numberOfTreasure += treasure.numberOfTreasure;
                }
                else
                {
                    TileMap[treasure.PosY, treasure.PosX] = treasure;
                }
            }
            else
                throw new Exception($"Cannot add {TileType.TREASURE} tile at position x: {treasure.PosX} y: {treasure.PosY} is out of range or already has a incompatible tile.");

        }

        public bool CheckIffOutOfRange(int x, int y)
        {
            if ((x >= 0 && x <= SizeX) && (y >= 0 && y <= SizeY))
            {
                return false;
            }
            return true;
        }
    }
}
