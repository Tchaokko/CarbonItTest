using CarteAuTresor.Interface;

namespace CarteAuTresor
{

    public class Treasure : ITile
    {
        public int numberOfTreasure { get; set; }
        public TileType tileType { get; set; }
        public int posY { get; set; }
        public int posX { get; set; }
        public bool gotAdventurer { get; set; }

        public Treasure(int treasureNumber, int x, int y)
        {
            numberOfTreasure = treasureNumber;
            posY = y;
            posX = x;
            tileType = TileType.TREASURE;
        }
    }

    public class Mountain : ITile
    {
        public TileType tileType { get; set; }
        public int posY { get; set; }
        public int posX { get; set; }
        public bool gotAdventurer { get; set; }

        public Mountain(int x, int y)
        {
            posY = y;
            posX = x;
            tileType = TileType.MOUNTAIN;
        }
    }

    public class Plain : ITile
    {
        public TileType tileType { get; set; }
        public int posY { get; set; }
        public int posX { get; set; }
        public bool gotAdventurer { get; set; }

        public Plain(int x, int y)
        {
            posX = x;
            posX = y;
            tileType = TileType.PLAIN;
        }
    }
}
