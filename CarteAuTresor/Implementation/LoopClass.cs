using System;
using System.Collections.Generic;
using CarteAuTresor.Interface;

namespace CarteAuTresor
{
    public class LoopClass
    {
        public IMap Map { get; set; }
        public List<IAdventurer> AdventurerList { get; set; }

        public LoopClass(IMap _map)
        {
            Map = _map;
        }

        public void InitializeGame(InstructionDto instruction)
        {
            foreach (var tile in instruction.tiles)
            {
                if (tile.tileType == TileType.MOUNTAIN)
                    Map.AddMountainToMap((Mountain)tile);
                else
                {
                    Map.AddTreasureToMap((Treasure)tile);
                }
            }
            AdventurerList = instruction.adventurer;
            AddPlayerToMap();

        }

        private void AddPlayerToMap()
        {
            foreach (var adventurer in AdventurerList)
            {
                if (!Map.CheckIffOutOfRange(adventurer.PosY, adventurer.PosX)
                    && Map.TileMap[adventurer.PosY, adventurer.PosX].tileType != TileType.MOUNTAIN
                    && Map.TileMap[adventurer.PosY, adventurer.PosX].GotAdventurer == false)
                {
                    Map.TileMap[adventurer.PosY, adventurer.PosX].GotAdventurer = true;
                }
                else
                    throw new Exception($"Cannot add adventurer {adventurer.Name} at position x: {adventurer.PosX} y: {adventurer.PosY} is out of range or has an incompatible tile");
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
                    if (adventurer.MovementList.Length > instructionIndex)
                    {
                        var movement = adventurer.MovementList[instructionIndex];
                        if (movement == 'A')
                        {
                            adventurer.movePlayerInFrontOfHim(Map);
                        }
                        else
                        {
                            if (movement == 'D')
                                adventurer.ChangeOrientation(MovementDirection.RIGHT);
                            else if (movement == 'G')
                                adventurer.ChangeOrientation(MovementDirection.LEFT);
                        }
                    }
                    else if (adventurer.MovementList.Length < instructionIndex
                        && !adventurer.FinishMoving)
                    {
                        instructionFinished += 1;
                        adventurer.FinishMoving = true;
                    }
                }
                instructionIndex++;
            }
            return Map;
        }
    }
}
