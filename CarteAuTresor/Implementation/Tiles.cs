using CarteAuTresor.Interface;

namespace CarteAuTresor
{

    public class Treasure : ITile
    {
        public int numberOfTreasure { get; set; }
        public TileType tileType { get; set; }
        public int PosY { get; set; }
        public int PosX { get; set; }
        public bool GotAdventurer { get; set; }

        public Treasure(int treasureNumber, int x, int y)
        {
            numberOfTreasure = treasureNumber;
            PosY = y;
            PosX = x;
            tileType = TileType.TREASURE;
        }
    }

    public class Mountain : ITile
    {
        public TileType tileType { get; set; }
        public int PosY { get; set; }
        public int PosX { get; set; }
        public bool GotAdventurer { get; set; }

        public Mountain(int x, int y)
        {
            PosY = y;
            PosX = x;
            tileType = TileType.MOUNTAIN;
        }
    }

    public class Plain : ITile
    {
        public TileType tileType { get; set; }
        public int PosY { get; set; }
        public int PosX { get; set; }
        public bool GotAdventurer { get; set; }

        public Plain(int x, int y)
        {
            PosX = x;
            PosX = y;
            tileType = TileType.PLAIN;
        }
    }
}
