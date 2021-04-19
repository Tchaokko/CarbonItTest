using CarteAuTresor.Interface;

namespace CarteAuTresor
{
    public class Adventurer : IAdventurer
    {
        public string name { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
        public string playerOrientation { get; set; }
        public string movementList { get; set; }
        public int treasures { get; set; }
        public bool finishMoving { get; set; }

        public Adventurer()
        {
            finishMoving = false;
        }

        public void movePlayerInFrontOfHim(IMap map)
        {
            switch (playerOrientation)
            {
                case "S":
                    if (!map.CheckIffOutOfRange(posY + 1, posX) && map.TileMap[posY + 1, posX].tileType != TileType.MOUNTAIN && !map.TileMap[posY + 1, posX].gotAdventurer)
                    {
                        map.TileMap[posY, posX].gotAdventurer = false;
                        posY = posY + 1;
                        TryAndGetTreasure(map);
                        map.TileMap[posY, posX].gotAdventurer = true;

                    }
                    break;
                case "N":
                    if (!map.CheckIffOutOfRange(posY - 1, posX) && map.TileMap[posY - 1, posX].tileType != TileType.MOUNTAIN && !map.TileMap[posY - 1, posX].gotAdventurer)
                    {
                        map.TileMap[posY, posX].gotAdventurer = false;
                        posY = posY - 1;
                        TryAndGetTreasure(map);
                        map.TileMap[posY, posX].gotAdventurer = true;

                    }
                    break;
                case "E":
                    if (!map.CheckIffOutOfRange(posY, posX + 1) && map.TileMap[posY, posX + 1].tileType != TileType.MOUNTAIN && !map.TileMap[posY, posX + 1].gotAdventurer)
                    {
                        map.TileMap[posY, posX].gotAdventurer = false;
                        posX = posX + 1;
                        TryAndGetTreasure(map);
                        map.TileMap[posY, posX].gotAdventurer = true;

                    }
                    break;
                case "O":
                    if (!map.CheckIffOutOfRange(posY, posX - 1) && map.TileMap[posY, posX - 1].tileType != TileType.MOUNTAIN && !map.TileMap[posY, posX - 1].gotAdventurer)
                    {
                        map.TileMap[posY, posX].gotAdventurer = false;
                        posX = posX - 1;
                        TryAndGetTreasure(map);
                        map.TileMap[posY, posX].gotAdventurer = true;
                    }
                    break;
                default:
                    break;
            }
        }

        private void TryAndGetTreasure(IMap map)
        {
            if (map.TileMap[posY, posX].tileType == TileType.TREASURE)
            {
                var treasure = (Treasure)map.TileMap[posY, posX];
                if (treasure.numberOfTreasure > 0)
                {
                    treasures += 1;
                    treasure.numberOfTreasure -= 1;
                }
            }
        }


        public void ChangeOrientation(MovementDirection movementDirection)
        {
            switch (playerOrientation)
            {
                case "S":
                    if (movementDirection == MovementDirection.RIGHT)
                    {
                        playerOrientation = "O";

                    }
                    else if (movementDirection == MovementDirection.LEFT)
                    {
                        playerOrientation = "E";

                    }
                    break;
                case "N":
                    if (movementDirection == MovementDirection.RIGHT)
                    {
                        playerOrientation = "E";

                    }
                    else if (movementDirection == MovementDirection.LEFT)
                    {
                        playerOrientation = "O";

                    }
                    break;
                case "E":
                    if (movementDirection == MovementDirection.RIGHT)
                    {
                        playerOrientation = "S";

                    }
                    else if (movementDirection == MovementDirection.LEFT)
                    {
                        playerOrientation = "N";

                    }
                    break;
                case "O":
                    if (movementDirection == MovementDirection.RIGHT)
                    {
                        playerOrientation = "N";

                    }
                    else if (movementDirection == MovementDirection.LEFT)
                    {
                        playerOrientation = "S";

                    }
                    break;
            }
        }

    }
}
