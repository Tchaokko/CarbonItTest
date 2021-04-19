using CarteAuTresor.Interface;

namespace CarteAuTresor
{
    public class Adventurer : IAdventurer
    {
        public string Name { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public string PlayerOrientation { get; set; }
        public string MovementList { get; set; }
        public int Treasures { get; set; }
        public bool FinishMoving { get; set; }

        public Adventurer()
        {
            FinishMoving = false;
        }

        public void movePlayerInFrontOfHim(IMap map)
        {
            switch (PlayerOrientation)
            {
                case "S":
                    if (!map.CheckIffOutOfRange(PosY + 1, PosX) && map.TileMap[PosY + 1, PosX].tileType != TileType.MOUNTAIN && !map.TileMap[PosY + 1, PosX].GotAdventurer)
                    {
                        MoveSouth(map);
                    }
                    break;
                case "N":
                    if (!map.CheckIffOutOfRange(PosY - 1, PosX) && map.TileMap[PosY - 1, PosX].tileType != TileType.MOUNTAIN && !map.TileMap[PosY - 1, PosX].GotAdventurer)
                    {

                        MoveNorth(map);
                    }
                    break;
                case "E":
                    if (!map.CheckIffOutOfRange(PosY, PosX + 1) && map.TileMap[PosY, PosX + 1].tileType != TileType.MOUNTAIN && !map.TileMap[PosY, PosX + 1].GotAdventurer)
                    {
                        MoveEast(map);
                    }
                    break;
                case "O":
                    if (!map.CheckIffOutOfRange(PosY, PosX - 1) && map.TileMap[PosY, PosX - 1].tileType != TileType.MOUNTAIN && !map.TileMap[PosY, PosX - 1].GotAdventurer)
                    {
                        MoveWest(map);
                    }
                    break;
                default:
                    break;
            }
        }

        private void MoveSouth(IMap map)
        {
            map.TileMap[PosY, PosX].GotAdventurer = false;
            PosY = PosY + 1;
            TryAndGetTreasure(map);
            map.TileMap[PosY, PosX].GotAdventurer = true;
        }

        private void MoveNorth(IMap map)
        {
            map.TileMap[PosY, PosX].GotAdventurer = false;
            PosY = PosY - 1;
            TryAndGetTreasure(map);
            map.TileMap[PosY, PosX].GotAdventurer = true;
        }

        private void MoveWest(IMap map)
        {
            map.TileMap[PosY, PosX].GotAdventurer = false;
            PosX = PosX - 1;
            TryAndGetTreasure(map);
            map.TileMap[PosY, PosX].GotAdventurer = true;
        }

        private void MoveEast(IMap map)
        {
            map.TileMap[PosY, PosX].GotAdventurer = false;
            PosX = PosX + 1;
            TryAndGetTreasure(map);
            map.TileMap[PosY, PosX].GotAdventurer = true;
        }

        private void TryAndGetTreasure(IMap map)
        {
            if (map.TileMap[PosY, PosX].tileType == TileType.TREASURE)
            {
                var treasure = (Treasure)map.TileMap[PosY, PosX];
                if (treasure.numberOfTreasure > 0)
                {
                    Treasures += 1;
                    treasure.numberOfTreasure -= 1;
                }
            }
        }


        public void ChangeOrientation(MovementDirection movementDirection)
        {
            switch (PlayerOrientation)
            {
                case "S":
                    if (movementDirection == MovementDirection.RIGHT)
                    {
                        PlayerOrientation = "O";

                    }
                    else if (movementDirection == MovementDirection.LEFT)
                    {
                        PlayerOrientation = "E";

                    }
                    break;
                case "N":
                    if (movementDirection == MovementDirection.RIGHT)
                    {
                        PlayerOrientation = "E";

                    }
                    else if (movementDirection == MovementDirection.LEFT)
                    {
                        PlayerOrientation = "O";

                    }
                    break;
                case "E":
                    if (movementDirection == MovementDirection.RIGHT)
                    {
                        PlayerOrientation = "S";

                    }
                    else if (movementDirection == MovementDirection.LEFT)
                    {
                        PlayerOrientation = "N";

                    }
                    break;
                case "O":
                    if (movementDirection == MovementDirection.RIGHT)
                    {
                        PlayerOrientation = "N";

                    }
                    else if (movementDirection == MovementDirection.LEFT)
                    {
                        PlayerOrientation = "S";

                    }
                    break;
            }
        }

    }
}
