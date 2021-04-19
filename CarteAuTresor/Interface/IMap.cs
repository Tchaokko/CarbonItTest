namespace CarteAuTresor.Interface
{
    public interface IMap
    {
        public void AddMountainToMap(Mountain mountain);
        public void AddTreasureToMap(Treasure treasure);
        public bool CheckIffOutOfRange(int x, int y);
        public ITile[,] TileMap { get; set; }
        public int sizeX { get; set; }
        public int sizeY { get; set; }

    }
}
