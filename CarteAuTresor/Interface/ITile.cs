namespace CarteAuTresor.Interface
{
    public interface ITile
    {
        int posX { get; set; }
        int posY { get; set; }
        TileType tileType { get; set; }
        bool gotAdventurer { get; set; }
    }
}
