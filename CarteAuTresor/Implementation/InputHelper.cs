using System;
using System.Linq;
using System.Text.RegularExpressions;
using CarteAuTresor.Interface;

namespace CarteAuTresor
{
    public class InputHelper
    {

        public InstructionDto instruction { get; set; }
        public IFileWrapper fileWrapper { get; set; }

        public InputHelper(IFileWrapper _fileWrapper)
        {
            fileWrapper = _fileWrapper;
        }

        public InstructionDto ReadFileAndPutIntoInstruction(string FileName)
        {
            instruction = new InstructionDto();
            var lines = fileWrapper.ReadAllLines(FileName);
            int lineNumber = 0;
            foreach (var line in lines)
            {
                ParseLine(line, lineNumber++);
            }
            return instruction;
        }

        private void ParseLine(string line, int lineNumber)
        {
            switch (line[0])
            {
                case 'M':
                    var (x, y) = ParseSimpleLine(line, lineNumber);
                    instruction.tiles.Add(new Mountain(x, y));
                    break;
                case 'C':
                    var (mapX, mapY) = ParseSimpleLine(line, lineNumber);
                    instruction.mapSizeX = mapX;
                    instruction.mapSizeY = mapY;
                    break;
                case 'T':
                    instruction.tiles.Add(ParseTreasureLine(line, lineNumber));
                    break;
                case 'A':
                    instruction.adventurer.Add(ParsePlayerInstructionLine(line, lineNumber));
                    break;
                case '#':
                    Console.WriteLine(string.Format("Ignoring line number {0}", lineNumber.ToString()));
                    break;
                default:
                    throw new Exception(string.Format("Line number {0} is not conform to the documentation", lineNumber.ToString()));
            }
        }


        private (int posX, int posY) ParseSimpleLine(string line, int lineNumber)
        {
            var splittedLine = line.Split('-').Select(p => p.Trim()).ToArray();

            if (splittedLine.Length == 3
                && int.TryParse(splittedLine[1], out var posX)
                && int.TryParse(splittedLine[2], out var posY))
            {
                return (posX, posY);

            }
            throw new Exception($"The line number {++lineNumber} is not valid");
        }

        private Adventurer ParsePlayerInstructionLine(string line, int lineNumber)
        {
            Regex directionRegex = new Regex("([NSEO])\\1*");
            //REDO REGEX! NO  GOOD
            Regex instructionRegex = new Regex("^[ADG]*$");
            var splittedLine = line.Split('-').Select(p => p.Trim()).ToArray();
            var directionStatus = directionRegex.IsMatch(splittedLine[4]);
            var instructionStatus = instructionRegex.IsMatch(splittedLine[5]);
            if (splittedLine.Length == 6
                && !int.TryParse(splittedLine[1], out _)
                && int.TryParse(splittedLine[2], out var posX)
                && int.TryParse(splittedLine[3], out var posY)
                && directionRegex.IsMatch(splittedLine[4])
                && instructionRegex.IsMatch(splittedLine[5]))
            {
                return new Adventurer { movementList = splittedLine[5], playerOrientation = splittedLine[4], posX = posX, posY = posY, name = splittedLine[1] };
            }
            throw new Exception($"The line number {lineNumber} is not valid");
        }

        private Treasure ParseTreasureLine(string line, int lineNumber)
        {
            var splittedLine = line.Split('-').Select(p => p.Trim()).ToArray();
            if (splittedLine.Length == 4
                && int.TryParse(splittedLine[1], out var posX)
                && int.TryParse(splittedLine[2], out var posY)
                && int.TryParse(splittedLine[3], out var number)
)
            {
                return new Treasure(number, posX, posY);


            }
            throw new Exception(string.Format("The line number {0} is not valid", ++lineNumber));
        }
    }
}
