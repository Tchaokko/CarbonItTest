using CarteAuTresor.Implementation;

namespace CarteAuTresor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                var fileWrapper = new FileWrapper();
                var inputHelper = new InputHelper(fileWrapper);
                var instruction = inputHelper.ReadFileAndPutIntoInstruction(args[0]);
                var loopClass = new LoopClass(new Map(instruction.mapSizeX, instruction.mapSizeY));
                loopClass.InitializeGame(instruction);
                var resultMap = loopClass.Loop();
                var stringToPrint = fileWrapper.WriteMapToStringArray(resultMap, instruction.adventurer);
                fileWrapper.WriteResultToFile(stringToPrint);

            }
        }
    }
}
