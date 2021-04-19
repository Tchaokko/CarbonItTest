namespace CarteAuTresor.Interface
{
    public interface IAdventurer
    {
        public void movePlayerInFrontOfHim(IMap map);
        public void ChangeOrientation(MovementDirection movementDirection);
        public string Name { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public string PlayerOrientation { get; set; }
        public string MovementList { get; set; }
        public int Treasures { get; set; }
        public bool FinishMoving { get; set; }


    }
}
