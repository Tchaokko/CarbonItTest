using System;
using System.Collections.Generic;
using CarteAuTresor.Interface;

namespace CarteAuTresor
{
    public class LoopClass
    {
        public IMap map { get; set; }
        public List<IAdventurer> AdventurerList { get; set; }

        public LoopClass(IMap _map)
        {
            map = _map;
        }

        public void InitializeGame(InstructionDto instruction)
        {
            foreach (var tile in instruction.tiles)
            {
                if (tile.tileType == TileType.MOUNTAIN)
                    map.AddMountainToMap((Mountain)tile);
                else
                {
                    map.AddTreasureToMap((Treasure)tile);
                }
            }
            AdventurerList = instruction.adventurer;
            AddPlayerToMap();

        }

        private void AddPlayerToMap()
        {
            foreach (var adventurer in AdventurerList)
            {
                if (!map.CheckIffOutOfRange(adventurer.posY, adventurer.posX)
                    && map.TileMap[adventurer.posY, adventurer.posX].tileType != TileType.MOUNTAIN
                    && map.TileMap[adventurer.posY, adventurer.posX].gotAdventurer == false)
                {
                    map.TileMap[adventurer.posY, adventurer.posX].gotAdventurer = true;
                }
                else
                    throw new Exception($"Cannot add adventurer {adventurer.name} at position x: {adventurer.posX} y: {adventurer.posY} is out of range or has an incompatible tile");
            }
        }

        public IMap Loop()
        {
            var instructionIndex = 0;
            var instructionFinished = 0;
            while (instructionFinished < AdventurerList.Count)
            {
                foreach (var adventurer in AdventurerList)
                {
                    if (adventurer.movementList.Length > instructionIndex)
                    {
                        var movement = adventurer.movementList[instructionIndex];
                        if (movement == 'A')
                        {
                            adventurer.movePlayerInFrontOfHim(map);
                        }
                        else
                        {
                            if (movement == 'D')
                                adventurer.ChangeOrientation(MovementDirection.RIGHT);
                            else if (movement == 'G')
                                adventurer.ChangeOrientation(MovementDirection.LEFT);
                        }
                    }
                    else if (adventurer.movementList.Length < instructionIndex
                        && !adventurer.finishMoving)
                    {
                        instructionFinished += 1;
                        adventurer.finishMoving = true;
                    }
                }
                instructionIndex++;
            }
            return map;
        }
    }
}
