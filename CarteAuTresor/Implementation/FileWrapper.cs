﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarteAuTresor.Interface;

namespace CarteAuTresor.Implementation
{
    public class FileWrapper : IFileWrapper
    {
        public string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }

        public void WriteResultToFile(string[] result)
        {
            File.WriteAllLines("Result.txt", result);
        }

        public string[] WriteMapToStringArray(IMap map, List<IAdventurer> adventurers)
        {
            List<string> result = new List<string>();
            List<string> lineForMap = new List<string>();
            List<string> playerLine = new List<string>();
            List<string> treasureLine = new List<string>();
            result.Add($"C - {map.sizeX} - {map.sizeY}");
            var numberOfSpaces = 1;

            foreach (var adventurer in adventurers)
            {
                playerLine.Add($"A - {adventurer.name} - {adventurer.posX} - {adventurer.posY} - {adventurer.playerOrientation} - {adventurer.treasures}");
                if (numberOfSpaces < adventurer.name.Length)
                    numberOfSpaces = adventurer.name.Length + 2;
            }
            for (int y = 0; y < map.sizeY; y++)
            {
                string[] line = new string[map.sizeX];
                for (int x = 0; x < map.sizeX; x++)
                {
                    if (map.TileMap[y, x].gotAdventurer)
                    {
                        foreach (var adventurer in adventurers)
                        {
                            if (adventurer.posX == x && adventurer.posY == y)
                            {
                                line[x] += $"A {adventurer.name}  ";
                            }
                        }
                    }
                    else
                    {
                        switch (map.TileMap[y, x].tileType)
                        {
                            case TileType.MOUNTAIN:
                                result.Add($"M - {x} - {y}");
                                line[x] += "M";
                                break;
                            case TileType.PLAIN:
                                line[x] += ".";
                                break;

                            case TileType.TREASURE:
                                var numberOfTreasure = (map.TileMap[y, x] as Treasure).numberOfTreasure;
                                treasureLine.Add($"T - {x} - {y} - {numberOfTreasure}");
                                line[x] += $"T {numberOfTreasure}";
                                break;
                        }
                        line[x] += string.Concat(Enumerable.Repeat(" ", numberOfSpaces));

                    }
                }
                lineForMap.Add(string.Concat(line));
            }
            result.Add("# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésors restants}");
            result.AddRange(treasureLine);
            result.Add("# {A comme Aventurier} - {Nom de l’aventurier} - {Axe horizontal} - {Axevertical} - {Orientation} - {Nb.trésors ramassés}");
            result.AddRange(playerLine);
            result.AddRange(lineForMap);

            return result.ToArray();
        }


    }
}
