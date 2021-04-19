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

        public void WriteMapToFile(IMap map, List<IAdventurer> adventurers)
        {
            List<string> lineForMap = new List<string>();
            List<string> introductoryLine = new List<string>();
            List<string> playerLine = new List<string>();
            introductoryLine.Add($"C - {map.sizeX} - {map.sizeY}");
            var numberOfSpaces = 1;

            foreach (var adventurer in adventurers)
            {
                playerLine.Add($"A - {adventurer.name} - {adventurer.posX} - {adventurer.posY} -  {adventurer.playerOrientation} - {adventurer.treasures}");
                if (numberOfSpaces < adventurer.name.Length)
                    numberOfSpaces = adventurer.name.Length + 2;
            }
            for (int y = 0; y < map.sizeX; y++)
            {
                string[] line = new string[map.sizeY];
                for (int x = 0; x < map.sizeY; x++)
                {
                    if (map.TileMap[y, x].gotAdventurer)
                    {
                        foreach (var adventurer in adventurers)
                        {
                            if (adventurer.posX == y && adventurer.posY == x)
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
                                introductoryLine.Add($"M - {x} - {y}");
                                line[x] += "M";
                                break;
                            case TileType.PLAIN:
                                line[x] += ".";
                                break;

                            case TileType.TREASURE:
                                var numberOfTreasure = (map.TileMap[y, x] as Treasure).numberOfTreasure;
                                introductoryLine.Add($"T - {x} - {y} - {numberOfTreasure}");
                                line[x] += $"T {numberOfTreasure}";
                                break;
                        }
                        line[x] += string.Concat(Enumerable.Repeat(" ", numberOfSpaces));

                    }
                }
                lineForMap.Add(string.Concat(line));
            }
            introductoryLine.AddRange(playerLine);
            introductoryLine.AddRange(lineForMap);
            File.WriteAllLines("Result.txt", introductoryLine.ToArray());
        }
    }
}