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
                    if (!map.CheckIffOutOfRange(posX, posY + 1) && map.TileMap[posX, posY + 1].tileType != TileType.MOUNTAIN && !map.TileMap[posX, posY + 1].gotAdventurer)
                    {
                        map.TileMap[posX, posY].gotAdventurer = false;
                        posY = posY + 1;
                        TryAndGetTreasure(map);
                        map.TileMap[posX, posY].gotAdventurer = true;

                    }
                    break;
                case "N":
                    if (!map.CheckIffOutOfRange(posX, posY - 1) && map.TileMap[posX, posY - 1].tileType != TileType.MOUNTAIN && !map.TileMap[posX, posY - 1].gotAdventurer)
                    {
                        map.TileMap[posX, posY].gotAdventurer = false;
                        posY = posY - 1;
                        TryAndGetTreasure(map);
                        map.TileMap[posX, posY].gotAdventurer = true;

                    }
                    break;
                case "E":
                    if (!map.CheckIffOutOfRange(posX + 1, posY) && map.TileMap[posX + 1, posY].tileType != TileType.MOUNTAIN && !map.TileMap[posX + 1, posY].gotAdventurer)
                    {
                        map.TileMap[posX, posY].gotAdventurer = false;
                        posX = posX + 1;
                        TryAndGetTreasure(map);
                        map.TileMap[posX, posY].gotAdventurer = true;

                    }
                    break;
                case "O":
                    if (!map.CheckIffOutOfRange(posX - 1, posY) && map.TileMap[posX - 1, posY].tileType != TileType.MOUNTAIN && !map.TileMap[posX + 1, posY].gotAdventurer)
                    {
                        map.TileMap[posX, posY].gotAdventurer = false;
                        posX = posX - 1;
                        TryAndGetTreasure(map);
                        map.TileMap[posX, posY].gotAdventurer = true;
                    }
                    break;
                default:
                    break;
            }
        }

        private void TryAndGetTreasure(IMap map)
        {
            if (map.TileMap[posX, posY].tileType == TileType.TREASURE)
            {
                var treasure = (Treasure)map.TileMap[posX, posY];
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
