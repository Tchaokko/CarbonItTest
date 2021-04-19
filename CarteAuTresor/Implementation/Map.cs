using System;
using CarteAuTresor.Interface;

namespace CarteAuTresor
{
    public class Map : IMap
    {

        public ITile[,] TileMap { get; set; }
        public int sizeX { get; set; }
        public int sizeY { get; set; }

        public Map(int sizeX, int sizeY)
        {
            if (sizeX < 0 && sizeX < 0)
                throw new Exception("The map size need to be bigger than 0");
            this.sizeX = sizeX;
            this.sizeY = sizeY;
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
            if (!CheckIffOutOfRange(mountain.posY, mountain.posX)
                && TileMap[mountain.posY, mountain.posX].tileType == TileType.PLAIN)
                TileMap[mountain.posY, mountain.posX] = mountain;
            else
                throw new Exception($"Cannot add {TileType.MOUNTAIN} tile at position x: {mountain.posX} y: {mountain.posY} is out of range or already has a incompatible tile.");

        }

        public void AddTreasureToMap(Treasure treasure)
        {
            if (!CheckIffOutOfRange(treasure.posY, treasure.posX)
                && TileMap[treasure.posY, treasure.posX].tileType != TileType.MOUNTAIN)
            {
                if (TileMap[treasure.posY, treasure.posX].tileType == TileType.TREASURE)
                {
                    var tempTreasure = (Treasure)TileMap[treasure.posY, treasure.posX];
                    tempTreasure.numberOfTreasure += treasure.numberOfTreasure;
                }
                else
                {
                    TileMap[treasure.posY, treasure.posX] = treasure;
                }
            }
            else
                throw new Exception($"Cannot add {TileType.TREASURE} tile at position x: {treasure.posX} y: {treasure.posY} is out of range or already has a incompatible tile.");

        }

        public bool CheckIffOutOfRange(int x, int y)
        {
            if ((x >= 0 && x <= sizeX) && (y >= 0 && y <= sizeY))
            {
                return false;
            }
            return true;
        }
    }
}
