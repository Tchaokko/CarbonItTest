namespace CarteAuTresor.Interface
{
    public interface IAdventurer
    {
        public void movePlayerInFrontOfHim(IMap map);
        public void ChangeOrientation(MovementDirection movementDirection);
        public string name { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
        public string playerOrientation { get; set; }
        public string movementList { get; set; }
        public int treasures { get; set; }
        public bool finishMoving { get; set; }


    }
}
