namespace CarteAuTresor.Interface
{
    public interface ITile
    {
        int PosX { get; set; }
        int PosY { get; set; }
        TileType tileType { get; set; }
        bool GotAdventurer { get; set; }
    }
}
