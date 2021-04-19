namespace CarteAuTresor.Interface
{
    public interface IMap
    {
        public void AddMountainToMap(Mountain mountain);
        public void AddTreasureToMap(Treasure treasure);
        public bool CheckIffOutOfRange(int x, int y);
        public ITile[,] TileMap { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }

    }
}
