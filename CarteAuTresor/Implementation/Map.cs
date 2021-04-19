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
            TileMap = new ITile[sizeX, sizeY];
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    TileMap[x, y] = new Plain(x, y);
                }
            }
        }

        public void AddMountainToMap(Mountain mountain)
        {
            if (!CheckIffOutOfRange(mountain.posX, mountain.posY)
                && TileMap[mountain.posX, mountain.posY].tileType == TileType.PLAIN)
                TileMap[mountain.posX, mountain.posY] = mountain;
            else
                throw new Exception($"Cannot add {TileType.MOUNTAIN} tile at position x: {mountain.posX} y: {mountain.posY} is out of range or already has a incompatible tile.");

        }

        public void AddTreasureToMap(Treasure treasure)
        {
            if (!CheckIffOutOfRange(treasure.posX, treasure.posY)
                && TileMap[treasure.posX, treasure.posY].tileType != TileType.MOUNTAIN)
            {
                if (TileMap[treasure.posX, treasure.posY].tileType == TileType.TREASURE)
                {
                    var tempTreasure = (Treasure)TileMap[treasure.posX, treasure.posY];
                    tempTreasure.numberOfTreasure += treasure.numberOfTreasure;
                }
                else
                {
                    TileMap[treasure.posX, treasure.posY] = treasure;
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
