using System;
using System.Linq;
using System.Text.RegularExpressions;
using CarteAuTresor.Interface;

namespace CarteAuTresor
{
    public class InputHelper
    {

        public InstructionDto Instruction { get; set; }
        public IFileWrapper FileWrapper { get; set; }

        public InputHelper(IFileWrapper _fileWrapper)
        {
            FileWrapper = _fileWrapper;
        }

        public InstructionDto ReadFileAndPutIntoInstruction(string FileName)
        {
            Instruction = new InstructionDto();
            var lines = FileWrapper.ReadAllLines(FileName);
            int lineNumber = 0;
            foreach (var line in lines)
            {
                ParseLine(line, lineNumber++);
            }
            return Instruction;
        }

        private void ParseLine(string line, int lineNumber)
        {
            switch (line[0])
            {
                case 'M':
                    var (x, y) = ParseSimpleLine(line, lineNumber);
                    Instruction.tiles.Add(new Mountain(x, y));
                    break;
                case 'C':
                    var (mapX, mapY) = ParseSimpleLine(line, lineNumber);
                    Instruction.mapSizeX = mapX;
                    Instruction.mapSizeY = mapY;
                    break;
                case 'T':
                    Instruction.tiles.Add(ParseTreasureLine(line, lineNumber));
                    break;
                case 'A':
                    Instruction.adventurer.Add(ParsePlayerInstructionLine(line, lineNumber));
                    break;
                case '#':
                    Console.WriteLine($"Ignoring line number {lineNumber}");
                    break;
                default:
                    throw new Exception(string.Format($"Line number {lineNumber} is not conform to the documentation"));
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
                return new Adventurer { MovementList = splittedLine[5], PlayerOrientation = splittedLine[4], PosX = posX, PosY = posY, Name = splittedLine[1] };
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
            throw new Exception($"The line number {++lineNumber} is not valid");
        }
    }
}
